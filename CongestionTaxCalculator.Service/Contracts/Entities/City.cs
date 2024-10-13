using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Service.Contracts.Entities;

public class City
{
    public int Id { get; private set; }
    public string Name { get; private set; }

    public ICollection<TaxRule> Rules { get; private set; }

    protected City()
    {
        //Just 4 Entity Framework
    }
    public City(string name)
    {
        ArgumentNullException.ThrowIfNull(name, "City name can not be empty.");
        Name = name;
    }
}
