using CongestionTaxCalculator.Service.Contracts;
using CongestionTaxCalculator.Service.Contracts.BaseClasses;
using CongestionTaxCalculator.Service.DataModels.DatabaseContext;
using CongestionTaxCalculator.Service.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Service.Extensions;

public static class BusinessDI
{
    public static IServiceCollection AddBusinessLayerDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddDbContext<CongestionTaxDb>(option => option.UseSqlServer(configuration.GetConnectionString("CongestionTaxDbContext")));
        services.AddDbContext<CongestionTaxDb>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("CongestionTaxDbContext"));
        });

        services.AddTransient<ICityTaxRuleService, CityTaxRulesService>();
        services.AddTransient<IVehicle, Car>();
        services.AddTransient<ITaxable, Car>();
        services.AddTransient<ITaxExempt, Motorbike>();
        return services;
    }
}
