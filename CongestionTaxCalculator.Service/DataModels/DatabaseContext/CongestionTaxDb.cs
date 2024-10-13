using CongestionTaxCalculator.Service.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Service.DataModels.DatabaseContext;

public class CongestionTaxDb : DbContext 
{   
    private bool _disposed = false;

    public CongestionTaxDb(DbContextOptions<CongestionTaxDb> options) : base(options)
    {

    }

    public virtual DbSet<City> Cities { get; set; }
    public virtual DbSet<TaxRule> TaxRules { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }


}
