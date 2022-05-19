using IPFTechnicalTest.Models;

namespace IPFTechnicalTest.Repository
{
    public interface IBeerRepository
    {
        Task<Bar> GetBar(int id);
        Task<List<Bar>> GetAllBars();
        Task<int> AddBar(Bar bar);
        Task<int> UpdateBar(Bar bar);

        Task<Beer> GetBeer(int id);
        Task<List<Beer>> GetAllBeers();
        Task<int> AddBeer(Beer beer);
        Task<int> UpdateBeer(Beer beer);
        Task<List<Beer>> GetBeerByAlcoholVolumeRange(decimal? gtVolume, decimal? ltVolume);

        Task<Brewery> GetBrewery(int id);
        Task<List<Brewery>> GetAllBreweries();
        Task<int> AddBrewery(Brewery brewery);
        Task<int> UpdateBrewery(Brewery brewery);

        Task<int> AddBarBeer(int barId, int beerId);
        Task<int> AddBreweryBeer(int breweryId, int beerId);
        Task<List<Bar>> GetAllBarsAndAssociatedBeers();
        Task<List<Beer>> GetBeersForBar(int barId);
    }
}
