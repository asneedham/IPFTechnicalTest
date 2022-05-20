using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

using IPFTechnicalTest.Models;

namespace IPFTechnicalTest.DataAccess
{
    public class BeerDbContext : DbContext, IBeerDbContext
    {
        public BeerDbContext(DbContextOptions<BeerDbContext> options) : base(options)
        {
        }

        public DbSet<Bar> Bar { get; set; }
        public DbSet<Beer> Beer { get; set; }
        public DbSet<Brewery> Brewery { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Bar>().HasMany(x => x.Beers).WithMany("Bars");

            builder.Entity<Brewery>().HasData(
                new Brewery
                {
                    BreweryId = 1,
                    Name = "Grupo Modelo"
                },
                new Brewery
                {
                    BreweryId = 2, 
                    Name = "Heineken N.V."
                }
            );

            builder.Entity<Beer>().HasData(
                new Beer
                {
                    BeerId = 1,
                    Name = "Corona",
                    PercentageAlcoholByVolume = 4.5m
                },
                new Beer
                {
                    BeerId = 2,
                    Name = "Modelo",
                    PercentageAlcoholByVolume = 4m
                },
                new Beer
                {
                    BeerId = 3,
                    Name = "Pacifico",
                    PercentageAlcoholByVolume = 3.5m
                },
                new Beer
                {
                    BeerId = 4,
                    Name = "Heineken",
                    PercentageAlcoholByVolume = 4.7m
                },
                new Beer
                {
                    BeerId = 5,
                    Name = "Amstel",
                    PercentageAlcoholByVolume = 4.8m
                }
            );

            builder.Entity<Bar>().HasData(
                new Bar
                {
                    BarId = 1,
                    Name = "All Bar One",
                    Address = "London"
                },
                new Bar
                {
                    BarId = 2,
                    Name = "The Pomeroy",
                    Address = "Amersham"
                }
            );
        }
    }
}
