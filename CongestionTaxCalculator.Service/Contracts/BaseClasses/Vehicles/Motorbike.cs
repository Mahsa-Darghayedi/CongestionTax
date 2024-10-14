namespace CongestionTaxCalculator.Service.Contracts.BaseClasses.Vehicles;

public class Motorbike : ITaxExempt
{
    public string GetVehicleType()
    {
        return "Motorbike";
    }
}