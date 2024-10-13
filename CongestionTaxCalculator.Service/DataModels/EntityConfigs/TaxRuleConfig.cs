using CongestionTaxCalculator.Service.Contracts.Entities;
using CongestionTaxCalculator.Service.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Service.DataModels.EntityConfigs;

internal class TaxRuleConfig : IEntityTypeConfiguration<TaxRule>
{
    public void Configure(EntityTypeBuilder<TaxRule> builder)
    {
        builder.ToTable("TaxRule");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();
        builder.Property(c => c.CityId).IsRequired();
        builder.Property(c => c.StartTime).IsRequired();
        builder.Property(c => c.EndTime).IsRequired();
        builder.Property(c => c.Tax).IsRequired();

    }
}
