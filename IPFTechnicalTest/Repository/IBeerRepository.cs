using IPFTechnicalTest.Models;

namespace IPFTechnicalTest.Repository
{
    public interface IBeerRepository
    {
        Task<Bar> GetBar(int id);
        Task<List<Bar>> GetAllBars();
        Task<int> AddBar(Bar bar);
        Task<int> UpdateBar(Bar bar);
        Task<bool> DeleteBar(int id);

        Task<Beer> GetBeer(int id);
        Task<List<Beer>> GetAllBeers();
        Task<int> AddBeer(Beer bar);
        Task<int> UpdateBeer(Beer beer);
        Task<bool> DeleteBeer(int id);
        Task<List<Beer>> GetBeerByAlcoholVolumeRange(decimal? gtVolume, decimal? ltVolume);

        Task<Brewery> GetBrewery(int id);
        Task<List<Brewery>> GetAllBreweries();
        Task<int> AddBrewery(Brewery brewery);
        Task<int> UpdateBrewery(Brewery brewery);
        Task<bool> DeleteBrewery(int id);
    }
}
