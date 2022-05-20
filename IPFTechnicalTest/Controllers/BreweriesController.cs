using Microsoft.AspNetCore.Mvc;

using IPFTechnicalTest.Models;
using IPFTechnicalTest.Repository;
using IPFTechnicalTest.ViewModels;

namespace IPFTechnicalTest.Controllers
{
    [Route("/brewery")]
    [ApiController]
    public class BreweriesController : ControllerBase
    {
        private readonly IBeerRepository _repository;

        public BreweriesController(IBeerRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BreweryViewModel>>> GetBrewery()
        {
            var dbBreweries = await _repository.GetAllBreweries();

            var breweries = new List<BreweryViewModel>();
            foreach (var dbBrewery in dbBreweries)
            {
                var brewery = new BreweryViewModel
                {
                    BreweryId = dbBrewery.BreweryId,
                    Name = dbBrewery.Name
                };

                foreach (var dbBeer in dbBrewery.Beers)
                {
                    var beer = new BeerViewModel
                    {
                        BeerId = dbBeer.BeerId,
                        Name = dbBeer.Name,
                        PercentageAlcoholByVolume = dbBeer.PercentageAlcoholByVolume,
                        BreweryId = dbBrewery.BreweryId
                    };

                    brewery.Beers.Add(beer);
                }

                breweries.Add(brewery);
            }

            return breweries;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BreweryViewModel>> GetBrewery(int id)
        {
            var dbBrewery = await _repository.GetBrewery(id);

            if(dbBrewery == null)
            {
                return new BreweryViewModel();
            }

            var brewery = new BreweryViewModel
            {
                BreweryId = dbBrewery.BreweryId,
                Name = dbBrewery.Name
            };

            foreach (var dbBeer in dbBrewery.Beers)
            {
                var beer = new BeerViewModel
                {
                    BeerId = dbBeer.BeerId,
                    Name = dbBeer.Name,
                    PercentageAlcoholByVolume = dbBeer.PercentageAlcoholByVolume,
                    BreweryId = dbBrewery.BreweryId
                };

                brewery.Beers.Add(beer);
            }

            return brewery;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrewery(int id, BreweryViewModel brewery)
        {
            if (id != brewery.BreweryId)
            {
                return BadRequest();
            }

            var dbBrewery = await _repository.GetBrewery(id);

            dbBrewery.Name = brewery.Name;

            var result = await _repository.UpdateBrewery(dbBrewery);

            if (result == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<BreweryViewModel>> PostBrewery(BreweryViewModel brewery)
        {
            var dbBrewery = new Brewery
            {
                Name = brewery.Name
            };

            int breweryId = await _repository.AddBrewery(dbBrewery);

            return CreatedAtAction(nameof(GetBrewery), new { id = breweryId }, brewery);
        }

        [HttpGet("beer")]
        public async Task<ActionResult<IEnumerable<BreweryViewModel>>> GetBreweriesWithBeers()
        {
            var dbBreweries = await _repository.GetAllBreweries();

            var breweryViewModels = new List<BreweryViewModel>();
            foreach (var dbBrewery in dbBreweries)
            {
                var brewery = new BreweryViewModel
                {
                    BreweryId = dbBrewery.BreweryId,
                    Name = dbBrewery.Name
                };

                foreach (var dbBeer in dbBrewery.Beers)
                {
                    var beer = new BeerViewModel
                    {
                        BeerId = dbBeer.BeerId,
                        Name = dbBeer.Name,
                        PercentageAlcoholByVolume = dbBeer.PercentageAlcoholByVolume,
                        BreweryId = brewery.BreweryId
                    };

                    brewery.Beers.Add(beer);
                }

                breweryViewModels.Add(brewery);
            }

            return breweryViewModels;
        }

        [HttpGet("{breweryId}/beer")]
        public async Task<ActionResult<BreweryViewModel>> GetBreweryWithBeers(int breweryId)
        {
            var dbBrewery = await _repository.GetBrewery(breweryId);

            var breweryViewModel = new BreweryViewModel
            {
                BreweryId = dbBrewery.BreweryId,
                Name = dbBrewery.Name
            };

            foreach (var dbBeer in dbBrewery.Beers)
            {
                var beer = new BeerViewModel
                {
                    BeerId = dbBeer.BeerId,
                    Name = dbBeer.Name,
                    PercentageAlcoholByVolume = dbBeer.PercentageAlcoholByVolume,
                    BreweryId = breweryViewModel.BreweryId
                };

                breweryViewModel.Beers.Add(beer);
            }

            return breweryViewModel;
        }

        [HttpPost("beer")]
        public async Task<ActionResult<int>> PostBreweryBeer(int breweryId, int beerId)
        {
            return await _repository.AddBreweryBeer(breweryId, beerId);
        }
    }
}
