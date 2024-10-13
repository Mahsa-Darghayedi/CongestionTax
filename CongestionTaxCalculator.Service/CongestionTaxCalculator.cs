using CongestionTaxCalculator.Service.Contracts.BaseClasses;
using CongestionTaxCalculator.Service.Contracts.Dtos;
using CongestionTaxCalculator.Service.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace CongestionTaxCalculator.Service;
public class CongestionTaxCalculator
{
    /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total congestion tax for that day
         */
    int totalFee = 0;
    private TimeSpan baseValue;
    private List<int> temp = [];
    public List<ResponseDto> GetTax(IVehicle vehicle, DateTime[] dates)
    {
        if (vehicle is ITaxExempt)
            return [];


        //   return ([], "This vehicle is tax free.");
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


    private int GetTollFee(TimeSpan date)
    {
        int hour = date.Hours;
        int minute = date.Minutes;

        return hour switch
        {
            6 => minute >= 0 && minute <= 29 ? 8 : 13,
            7 => 18,
            8 => minute >= 0 && minute <= 29 ? 13 : 8,
            < 15 => 8,
            15 => minute >= 0 && minute <= 29 ? 13 : 18,
            16 => 18,
            17 => 13,
            18 => minute >= 0 && minute <= 29 ? 8 : 0,
            _ => 0
        };
    }

}
