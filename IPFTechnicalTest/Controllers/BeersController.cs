using Microsoft.AspNetCore.Mvc;

using IPFTechnicalTest.Models;
using IPFTechnicalTest.Repository;
using IPFTechnicalTest.ViewModels;

namespace IPFTechnicalTest.Controllers
{
    [Route("/beer")]
    [ApiController]
    public class BeersController : ControllerBase
    {
        private readonly IBeerRepository _repository;

        public BeersController(IBeerRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BeerViewModel>> GetBeer(int id)
        {
            var dbBeer = await _repository.GetBeer(id);

            var beer = new BeerViewModel()
            {
                BeerId = dbBeer.BeerId,
                BreweryId = dbBeer.BreweryId.HasValue ? dbBeer.BreweryId.Value : 0,
                Name = dbBeer.Name,
                PercentageAlcoholByVolume = dbBeer.PercentageAlcoholByVolume
            };

            return beer;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutBeer(int id, BeerViewModel beer)
        {
            if (id != beer.BeerId)
            {
                return BadRequest();
            }

            var dbBeer = await _repository.GetBeer(id);

            dbBeer.PercentageAlcoholByVolume = beer.PercentageAlcoholByVolume;
            dbBeer.Name = beer.Name;
            
            var result = await _repository.UpdateBeer(dbBeer);

            if (result == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost()]
        public async Task<ActionResult<BeerViewModel>> PostBeer(BeerViewModel beer)
        {
            var dbBeer = new Beer
            {
                BeerId = beer.BeerId,
                Name = beer.Name,
                PercentageAlcoholByVolume = beer.PercentageAlcoholByVolume
            };

            int beerId = await _repository.AddBeer(dbBeer);

            return CreatedAtAction(nameof(GetBeer), new { id = beerId }, beer);
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<BeerViewModel>>> GetBeerByAlcoholVolumeRange(decimal? gtAlcoholByVolume, decimal? ltAlcoholByVolume)
        {
            var dbBeersWithinRange = await _repository.GetBeerByAlcoholVolumeRange(gtAlcoholByVolume, ltAlcoholByVolume);

            if (dbBeersWithinRange == null)
            {
                return NotFound();
            }

            var beersWithinRange = new List<BeerViewModel>();
            foreach(var dbBeer in dbBeersWithinRange)
            {
                var beer = new BeerViewModel
                {
                    BeerId = dbBeer.BeerId,
                    BreweryId = dbBeer.BreweryId.HasValue ? dbBeer.BreweryId.Value : 0,
                    Name = dbBeer.Name,
                    PercentageAlcoholByVolume = dbBeer.PercentageAlcoholByVolume
                };

                beersWithinRange.Add(beer);
            }

            return beersWithinRange;
        }
    }
}
