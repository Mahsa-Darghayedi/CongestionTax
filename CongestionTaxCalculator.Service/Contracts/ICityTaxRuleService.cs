using CongestionTaxCalculator.Service.Contracts.BaseClasses;
using CongestionTaxCalculator.Service.Contracts.Dtos;
using CongestionTaxCalculator.Service.Contracts.Dtos.Enums;
using CongestionTaxCalculator.Service.Contracts.Dtos.TaxRule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Service.Contracts;

public interface ICityTaxRuleService
{
    Task AddCity(string cityName);
    Task AddTaxRules(CityTaxRuleCreationDto dto);
    Task<List<ResponseDto>> GetTax(string cityName, VehiclesType vehicle, DateTime[] dates);
    //Task<int> GetTaxByCity(string cityName, TimeSpan time);
}
