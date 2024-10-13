using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Service.Contracts.Entities;

public class TaxRule
{
    public int Id { get; set; }
    public int CityId { get; private set; }
    public string StartTime { get; private set; }
    public string EndTime { get; private set; }
    public int Tax { get; private set; }

    public City City { get; private set; }
    protected TaxRule()
    {
        //Just 4 EF
    }
    public TaxRule(int _cityId, string _start, string _end, int _tax)
    {
        //Validation
        CityId = _cityId;
        StartTime = _start;
        EndTime = _end;
        Tax = _tax;
    }

}
