﻿using FishMarket.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace FishMarket.Data.Mapping
{
    public class FishMap : IEntityTypeConfiguration<Fish>
    {
        public void Configure(EntityTypeBuilder<Fish> modelBuilder)
        {
            modelBuilder.HasKey(a => a.Id);
            modelBuilder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            modelBuilder.Property(x => x.Price).IsRequired().HasPrecision(18, 2);

            modelBuilder.ToTable("Fish");
        }
    }
}
