namespace CongestionTaxCalculator.Service.Contracts.BaseClasses.Vehicles;

public class Car : ITaxable
{
    public string GetVehicleType()
    {
        return "Car";
    }
}