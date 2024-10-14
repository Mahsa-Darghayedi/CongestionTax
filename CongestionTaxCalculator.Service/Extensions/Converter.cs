using CongestionTaxCalculator.Service.Contracts.BaseClasses;
using CongestionTaxCalculator.Service.Contracts.Dtos.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CongestionTaxCalculator.Service.Extensions.CustomExceptions;


namespace CongestionTaxCalculator.Service.Extensions;

public static class Converter
{
    public static TimeSpan ConvertToTimeSpan(this string value)
    {
        DateTime dt = DateTime.Parse(value);
        return dt.TimeOfDay;
    }



    public static bool IsTollFreeDate(this DateTime date)
    {
        int year = date.Year;
        int month = date.Month;
        int day = date.Day;

        if (month is 7) return true;
        if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) return true;

        if (year == 2013)
        {
            if (month == 1 && day == 1 ||
                month == 3 && (day == 28 || day == 29) ||
                month == 4 && (day == 1 || day == 30) ||
                month == 5 && (day == 1 || day == 8 || day == 9) ||
                month == 6 && (day == 5 || day == 6 || day == 21) ||
                month == 7 ||
                month == 11 && day == 1 ||
                month == 12 && (day == 24 || day == 25 || day == 26 || day == 31))
            {
                return true;
            }
        }
        return false;
    }


    public static bool IsTaxable(this VehiclesType vehicle)
    {
        var vehicleTypeStr = vehicle.ToString();
        Exceptions.ItemNotFoundException.ThrowIfNull(vehicleTypeStr);
        var type = Type.GetType($"CongestionTaxCalculator.Service.Contracts.BaseClasses.Vehicles.{vehicleTypeStr}");
        Exceptions.BadRequestException.ThrowIfNull(type);
        var implementedInterface = type!.GetInterfaces();
        if (implementedInterface.ToList().Contains(typeof(ITaxExempt)))
            return false;
        else
            return true;
    }


}
