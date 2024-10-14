using CongestionTaxCalculator.Service.Contracts;
using CongestionTaxCalculator.Service.Contracts.BaseClasses;
using CongestionTaxCalculator.Service.Contracts.Dtos;
using CongestionTaxCalculator.Service.Contracts.Dtos.Enums;
using CongestionTaxCalculator.Service.Contracts.Dtos.TaxRule;
using CongestionTaxCalculator.Service.Contracts.Entities;
using CongestionTaxCalculator.Service.DataModels.DatabaseContext;
using CongestionTaxCalculator.Service.Extensions;
using CongestionTaxCalculator.Service.Extensions.CustomExceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CongestionTaxCalculator.Service.Implementation;

internal class CityTaxRulesService : ICityTaxRuleService
{
    private int totalFee = 0;
    private TimeSpan baseValue;
    private List<int> temp = [];
    private List<TaxRule> _cityTaxRules = [];
    private readonly CongestionTaxDb _dbContext;
    public CityTaxRulesService(CongestionTaxDb context)
    {
        _dbContext = context;
    }
    public async Task AddCity(string cityName)
    {
        Exceptions.BadRequestException.ThrowIfNull(cityName);
        using var context = _dbContext;
        await _dbContext.Cities.AddAsync(new City(cityName));
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddTaxRules(CityTaxRuleCreationDto dto)
    {

        var city = await _dbContext.Cities.FirstOrDefaultAsync(c => c.Name.Equals(dto.City));
        Exceptions.ItemNotFoundException.ThrowIfNull(city);
        List<TaxRule> rules = [];
        dto.TaxRules.ForEach(c =>
        {
            TaxRule rule = new(city!.Id, c.StartTime, c.EndTime, c.Tax);
            _dbContext.TaxRules.Add(rule);
        });
        await _dbContext.SaveChangesAsync();

    }

    public async Task<List<ResponseDto>> GetTax(string cityName, VehiclesType vehicle, DateTime[] dates)
    {
        Exceptions.TaxFreeException.ThrowIf(!vehicle.IsTaxable());
        var city = await _dbContext.Cities.FirstOrDefaultAsync(c => c.Name.Equals(cityName));
        Exceptions.ItemNotFoundException.ThrowIfNull(city);
        _cityTaxRules = await _dbContext.TaxRules.Where(c => c.CityId.Equals(city!.Id)).ToListAsync();
        List<ResponseDto> result = [];
        dates.Where(c => c.Date.IsTollFreeDate()).ToList().ForEach(d =>
               result.Add(new ResponseDto(d.Date.ToString(), 0))
        );
        var notHolidays = dates.Where(c => !c.Date.IsTollFreeDate()).ToList();
        var allDates = notHolidays.GroupBy(c => c.Date).Select(g => (g.Key, g.Select(c => c.TimeOfDay).ToArray())).ToList();
        allDates.ForEach(date =>
        {
            totalFee = 0;
            if (date.Item2.Length > 1)
            {
                temp.Clear();
                baseValue = date.Item2[0];
                temp.Add(GetTollFee(baseValue));
                GetTaxRecursive(date.Item2[1..]);
            }
            else
                totalFee = GetTollFee(date.Item2[0]);
            result.Add(new ResponseDto(date.Key.Date.ToString(), totalFee > 60 ? 60 : totalFee));
        });
        return result;
    }

    private int GetTaxRecursive(TimeSpan[] times)
    {
        if (times.Length > 1)
        {
            TimeSpan next = times[0];
            if (next.TotalMinutes - baseValue.TotalMinutes <= 60)
            {
                temp.Add(GetTollFee(next));
                return GetTaxRecursive(times[1..]);
            }
        }
        totalFee += temp.Max();
        temp.Clear();
        baseValue = times[0];
        temp.Add(GetTollFee(baseValue));
        if (times.Length > 1)
        {
            return GetTaxRecursive(times[1..]);
        }
        else
            totalFee += GetTollFee(baseValue);

        return totalFee;
    }

    private int GetTollFee(TimeSpan time)
    {
        return _cityTaxRules.Where(c => (c.StartTime.ConvertToTimeSpan() <= time && c.EndTime.ConvertToTimeSpan() >= time)).Select(c => c.Tax).FirstOrDefault();
    }


    //private int GetTollFee(TimeSpan date)
    //{
    //    int hour = date.Hours;
    //    int minute = date.Minutes;

    //    return hour switch
    //    {
    //        6 => minute >= 0 && minute <= 29 ? 8 : 13,
    //        7 => 18,
    //        8 => minute >= 0 && minute <= 29 ? 13 : 8,
    //        < 15 => 8,
    //        15 => minute >= 0 && minute <= 29 ? 13 : 18,
    //        16 => 18,
    //        17 => 13,
    //        18 => minute >= 0 && minute <= 29 ? 8 : 0,
    //        _ => 0
    //    };
    //}



}
