using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Service.Contracts.Dtos.TaxRule;

public class CityTaxRuleCreationDto
{
    public string City { get; init; }
    public List<TaxRuleCreationDto> TaxRules { get; init; }
}
