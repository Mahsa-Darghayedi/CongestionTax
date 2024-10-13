using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Service.Contracts.Dtos.TaxRule;

public record TaxRuleCreationDto
{
    public string StartTime { get; init; }
    public string EndTime { get; init; }
    public int Tax { get; init; }
}
