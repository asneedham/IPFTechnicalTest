﻿// <auto-generated />
using System;
using IPFTechnicalTest.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IPFTechnicalTest.Migrations
{
    [DbContext(typeof(BeerDbContext))]
    [Migration("20220516083350_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.5");

            modelBuilder.Entity("IPFTechnicalTest.Models.Bar", b =>
                {
                    b.Property<int>("BarId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("BarId");

                    b.ToTable("Bar");

                    b.HasData(
                        new
                        {
                            BarId = 1,
                            Address = "London",
                            Name = "All Bar One"
                        },
                        new
                        {
                            BarId = 2,
                            Address = "Amersham",
                            Name = "The Pomeroy"
                        });
                });

            modelBuilder.Entity("IPFTechnicalTest.Models.Beer", b =>
                {
                    b.Property<int>("BeerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BarId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BreweryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("PercentageAlcoholByVolume")
                        .HasColumnType("TEXT");

                    b.HasKey("BeerId");

                    b.HasIndex("BarId");

                    b.HasIndex("BreweryId");

                    b.ToTable("Beer");

                    b.HasData(
                        new
                        {
                            BeerId = 1,
                            Name = "Corona",
                            PercentageAlcoholByVolume = 4.5m
                        },
                        new
                        {
                            BeerId = 2,
                            Name = "Modelo",
                            PercentageAlcoholByVolume = 4m
                        },
                        new
                        {
                            BeerId = 3,
                            Name = "Pacifico",
                            PercentageAlcoholByVolume = 3.5m
                        },
                        new
                        {
                            BeerId = 4,
                            Name = "Heineken",
                            PercentageAlcoholByVolume = 4.7m
                        },
                        new
                        {
                            BeerId = 5,
                            Name = "Amstel",
                            PercentageAlcoholByVolume = 4.8m
                        });
                });

            modelBuilder.Entity("IPFTechnicalTest.Models.Brewery", b =>
                {
                    b.Property<int>("BreweryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("BreweryId");

                    b.ToTable("Brewery");

                    b.HasData(
                        new
                        {
                            BreweryId = 1,
                            Name = "Grupo Modelo"
                        },
                        new
                        {
                            BreweryId = 2,
                            Name = "Heineken N.V."
                        });
                });

            modelBuilder.Entity("IPFTechnicalTest.Models.Beer", b =>
                {
                    b.HasOne("IPFTechnicalTest.Models.Bar", null)
                        .WithMany("Beers")
                        .HasForeignKey("BarId");

                    b.HasOne("IPFTechnicalTest.Models.Brewery", null)
                        .WithMany("Beers")
                        .HasForeignKey("BreweryId");
                });

            modelBuilder.Entity("IPFTechnicalTest.Models.Bar", b =>
                {
                    b.Navigation("Beers");
                });

            modelBuilder.Entity("IPFTechnicalTest.Models.Brewery", b =>
                {
                    b.Navigation("Beers");
                });
#pragma warning restore 612, 618
        }
    }
}
