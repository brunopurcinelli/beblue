﻿// <auto-generated />
using System;
using BeBlueApi.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BeBlueApi.Infra.Data.Migrations
{
    [DbContext(typeof(BeblueDbContext))]
    [Migration("20190215140316_BeblueSQL")]
    partial class BeblueSQL
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BeBlueApi.Domain.Models.Cashback", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("IdGender");

                    b.Property<decimal>("Percent");

                    b.Property<string>("WeekDay")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("IdGender");

                    b.ToTable("Cashback");
                });

            modelBuilder.Entity("BeBlueApi.Domain.Models.DiscMusic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("IdGender");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.HasIndex("IdGender");

                    b.ToTable("DiscMusic");
                });

            modelBuilder.Entity("BeBlueApi.Domain.Models.MusicGender", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(250)")
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.ToTable("MusicGender");
                });

            modelBuilder.Entity("BeBlueApi.Domain.Models.Sales", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<DateTime>("SalesDate");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("numeric(18,2)");

                    b.Property<decimal>("TotalCashback")
                        .HasColumnType("numeric(18,2)");

                    b.HasKey("Id");

                    b.ToTable("Sales");
                });

            modelBuilder.Entity("BeBlueApi.Domain.Models.SalesLine", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Cashback");

                    b.Property<string>("DiscName")
                        .HasMaxLength(250);

                    b.Property<Guid>("IdItem");

                    b.Property<Guid>("IdSales");

                    b.Property<decimal>("PriceUnit");

                    b.Property<int>("Quantity");

                    b.Property<decimal>("SalesPrice");

                    b.HasKey("Id");

                    b.HasIndex("IdItem");

                    b.HasIndex("IdSales");

                    b.ToTable("SalesLine");
                });

            modelBuilder.Entity("BeBlueApi.Domain.Models.Cashback", b =>
                {
                    b.HasOne("BeBlueApi.Domain.Models.MusicGender", "MusicGender")
                        .WithMany("Cashbacks")
                        .HasForeignKey("IdGender")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BeBlueApi.Domain.Models.DiscMusic", b =>
                {
                    b.HasOne("BeBlueApi.Domain.Models.MusicGender", "MusicGender")
                        .WithMany("DiscMusics")
                        .HasForeignKey("IdGender")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BeBlueApi.Domain.Models.SalesLine", b =>
                {
                    b.HasOne("BeBlueApi.Domain.Models.DiscMusic", "DiscMusic")
                        .WithMany("SalesLines")
                        .HasForeignKey("IdItem")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BeBlueApi.Domain.Models.Sales", "Sales")
                        .WithMany("Lines")
                        .HasForeignKey("IdSales")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
