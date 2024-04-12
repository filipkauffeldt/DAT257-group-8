using System.Collections.Generic;
using API.Contracts;
using API.Model.ObjectModels;
using Microsoft.EntityFrameworkCore;
using EFCore.NamingConventions;

namespace API.Model
{
    public class CountryDbContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Data> Data { get; set; }
        public DbSet<DataPoint> DataPoints { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options
                .UseNpgsql("Host=127.0.0.1; Port=8001; Database=countries; Username=postgres; Password=docker")
                .UseLowerCaseNamingConvention();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(c => c.Code);

                entity
                    .Property(c => c.Name)
                    .IsRequired();

                entity
                    .HasMany(c => c.Data)
                    .WithOne()
                    .HasForeignKey("country")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Data>(entity =>
            {
                entity.HasKey(d => d.D_Id);

                entity
                    .Property(d => d.Name)
                    .IsRequired();

                entity
                    .Property(d => d.Unit)
                    .IsRequired();

                entity
                    .Property(d => d.D_Id)
                    .UseSerialColumn();

                entity
                    .HasMany(d => d.Points)
                    .WithOne()
                    .HasForeignKey("dp_id")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<DataPoint>(entity =>
            {
                entity.HasKey(dp => dp.DP_Id);

                entity
                    .Property(dp => dp.Date)
                    .IsRequired();

                entity
                    .Property(dp => dp.Value)
                    .IsRequired();

                entity
                    .Property(dp => dp.DP_Id)
                    .UseSerialColumn();
            });
        }
    }

}