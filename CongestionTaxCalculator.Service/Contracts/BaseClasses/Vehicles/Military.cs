using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Service.Contracts.BaseClasses.Vehicles;

public class Military : ITaxExempt
{
    public string GetVehicleType()
        => "Military";


}
