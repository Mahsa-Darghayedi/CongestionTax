using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Service.Contracts.BaseClasses.Vehicles;

public class Diplomat : ITaxExempt
{
    public string GetVehicleType()
    => "Diplomat";
}
