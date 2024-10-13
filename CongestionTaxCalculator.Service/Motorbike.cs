using CongestionTaxCalculator.Service.Contracts.BaseClasses;

namespace CongestionTaxCalculator.Service;

public class Motorbike : ITaxExempt
{
    public string GetVehicleType()
    {
        return "Motorbike";
    }
}