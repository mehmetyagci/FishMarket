using FishMarket.Data.Mapping;
using FishMarket.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishMarket.Data
{
    public class FishMarketDbContext : DbContext
    {
        public FishMarketDbContext(DbContextOptions<FishMarketDbContext> options) :
            base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new FishMap());

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Email = "mehmetyagci53@gmail.com", Password = "mehmetyagci53@gmail.com", IsEmailVerified = true },
                new User { Id = 2, Email = "admin@fishmarket.com", Password = "admin@fishmarket.com", IsEmailVerified = true }
            );

            modelBuilder.Entity<Fish>().HasData(
                new Fish { Id = 1, Name = "Hamsi", Price = 100.10M, Image = "60c39b56-ef92-4411-96d8-45a33d059f50.jpg" },
                new Fish { Id = 2, Name = "Levrek", Price = 200.20M, Image = "53df3af8-19e2-4836-b3a2-e6c70ec7ec17.png" },
                new Fish { Id = 3, Name = "Lüfer", Price = 300.30M, Image = "611e29ba-b49b-404b-a1da-3ebb9026c5cc.jpg" },
                new Fish { Id = 4, Name = "Somon", Price = 400.40M, Image = "150eafe9-360e-4208-9647-2a96a88e964b.jpg" }
            );

            base.OnModelCreating(modelBuilder);
        }
        public override int SaveChanges()
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReference)
                {
                    switch (item.Entity)
                    {
                        case EntityState.Added:
                            {
                                entityReference.CreatedDate = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                Entry(entityReference).Property(x => x.CreatedDate).IsModified = false;
                                entityReference.UpdatedDate = DateTime.Now;
                                break;
                            }
                    }
                }
            }
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries())
            {
                if (item.Entity is BaseEntity entityReference)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            {
                                entityReference.CreatedDate = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:
                            {
                                Entry(entityReference).Property(x => x.CreatedDate).IsModified = false;
                                entityReference.UpdatedDate = DateTime.Now;
                                break;
                            }
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Fish> Fishes { get; set; }
    }
}
