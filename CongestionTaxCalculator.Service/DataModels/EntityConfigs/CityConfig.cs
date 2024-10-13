using CongestionTaxCalculator.Service.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongestionTaxCalculator.Service.DataModels.EntityConfigs;

internal class CityConfig : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("City");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).ValueGeneratedOnAdd();
        builder.Property(c => c.Name).IsRequired().HasMaxLength(50).IsUnicode(false);

        builder.HasMany(c => c.Rules)
            .WithOne(c => c.City)
            .HasForeignKey(c => c.CityId);
    }
}
