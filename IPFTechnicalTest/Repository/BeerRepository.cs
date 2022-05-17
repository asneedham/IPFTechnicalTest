using Microsoft.EntityFrameworkCore;

using IPFTechnicalTest.Models;
using IPFTechnicalTest.DataAccess;

namespace IPFTechnicalTest.Repository
{
    public class BeerRepository : IBeerRepository
    {
        private readonly IBeerDbContext _dbContext;

        public BeerRepository(IBeerDbContext context) => _dbContext = context;

        public async Task<List<Bar>> GetAllBars()
        {
            return await _dbContext.Bar.ToListAsync();
        }

        public async Task<Bar> GetBar(int id)
        {
            return await _dbContext.Bar.FindAsync(id);
        }

        public async Task<int> AddBar(Bar bar)
        {
            _dbContext.Bar.Add(bar);
            return await _dbContext.SaveChangesAsync();
        }
        public async Task<int> UpdateBar(Bar bar)
        {
            _dbContext.Entry(bar).State = EntityState.Modified;

            try
            {
                return await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var barExists = (_dbContext.Bar?.Any(e => e.BarId == bar.BarId)).GetValueOrDefault();

                if (!barExists)
                {
                    return 0;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteBar(int id)
        {
            var bar = await _dbContext.Bar.FindAsync(id);
            
            _dbContext.Bar.Remove(bar);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<Beer>> GetAllBeers()
        {
            return await _dbContext.Beer.ToListAsync();
        }

        public async Task<Beer> GetBeer(int id)
        {
            return await _dbContext.Beer.FindAsync(id);
        }

        public async Task<int> AddBeer(Beer Beer)
        {
            _dbContext.Beer.Add(Beer);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateBeer(Beer beer)
        {
            _dbContext.Entry(beer).State = EntityState.Modified;

            try
            {
                return await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var beerExists = (_dbContext.Beer?.Any(e => e.BeerId == beer.BeerId)).GetValueOrDefault();

                if (!beerExists)
                {
                    return 0;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteBeer(int id)
        {
            var beer = await _dbContext.Beer.FindAsync(id);

            _dbContext.Beer.Remove(beer);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<Beer>> GetBeerByAlcoholVolumeRange(decimal? gtVolume, decimal? ltVolume)
        {
            if (_dbContext.Beer == null)
            {
                return null;
            }

            if (gtVolume.HasValue && ltVolume.HasValue)
            {
                return await _dbContext.Beer.Where(x => x.PercentageAlcoholByVolume > gtVolume && x.PercentageAlcoholByVolume < ltVolume).ToListAsync();
            }

            if (gtVolume.HasValue)
            {
                return await _dbContext.Beer.Where(x => x.PercentageAlcoholByVolume > gtVolume).ToListAsync();
            }

            return await _dbContext.Beer.Where(x => x.PercentageAlcoholByVolume < ltVolume).ToListAsync();
        }

        public async Task<List<Brewery>> GetAllBreweries()
        {
            return await _dbContext.Brewery.ToListAsync();
        }

        public async Task<Brewery> GetBrewery(int id)
        {
            return await _dbContext.Brewery.FindAsync(id);
        }

        public async Task<int> AddBrewery(Brewery Brewery)
        {
            _dbContext.Brewery.Add(Brewery);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateBrewery(Brewery Brewery)
        {
            _dbContext.Entry(Brewery).State = EntityState.Modified;

            try
            {
                return await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                var BreweryExists = (_dbContext.Brewery?.Any(e => e.BreweryId == Brewery.BreweryId)).GetValueOrDefault();

                if (!BreweryExists)
                {
                    return 0;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<bool> DeleteBrewery(int id)
        {
            var brewery = await _dbContext.Brewery.FindAsync(id);

            _dbContext.Brewery.Remove(brewery);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
