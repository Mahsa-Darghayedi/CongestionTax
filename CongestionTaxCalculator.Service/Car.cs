using CongestionTaxCalculator.Service.Contracts.BaseClasses;

namespace CongestionTaxCalculator.Service;

public class Car : ITaxable
{
    public string GetVehicleType()
    {
        return "Car";
    }
}