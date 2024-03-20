using FishMarket.Domain;
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
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> modelBuilder)
        {
            modelBuilder.HasKey(x => x.Id);
            modelBuilder.HasIndex(x => x.Email).IsUnique(true);
            modelBuilder.Property(x => x.Email).IsRequired().HasMaxLength(50);
            modelBuilder.Property(x => x.Password).IsRequired().HasMaxLength(100);
            modelBuilder.ToTable("User");
        }
    }
}
