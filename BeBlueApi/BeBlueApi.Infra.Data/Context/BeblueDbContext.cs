using BeBlueApi.Domain.Models;
using BeBlueApi.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BeBlueApi.Infra.Data.Context
{
    public class BeblueDbContext : DbContext
    {
        //public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DiscMusicMap());
            modelBuilder.ApplyConfiguration(new MusicGenderMap());
            modelBuilder.ApplyConfiguration(new CashbackMap());
            modelBuilder.ApplyConfiguration(new SalesMap());
            modelBuilder.ApplyConfiguration(new SalesLineMap());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }
        public DbSet<DiscMusic> DiscMusic { get; set; }

        public DbSet<Cashback> Cashback { get; set; }

        public DbSet<MusicGender> MusicGender { get; set; }

        public DbSet<Sales> Sales { get; set; }

        public DbSet<SalesLine> SalesLine { get; set; }
    }
}
