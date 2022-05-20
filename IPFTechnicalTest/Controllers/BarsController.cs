using Microsoft.AspNetCore.Mvc;

using IPFTechnicalTest.Models;
using IPFTechnicalTest.Repository;
using IPFTechnicalTest.ViewModels;

namespace IPFTechnicalTest.Controllers
{
    [Route("/bar")]
    [ApiController]
    public class BarsController : ControllerBase
    {
        private readonly IBeerRepository _repository;

        public BarsController(IBeerRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BarViewModel>>> GetBar()
        {
            var dbBars = await _repository.GetAllBars();

            var bars = new List<BarViewModel>();
            foreach(var dbBar in dbBars)
            {
                var bar = new BarViewModel
                {
                    BarId = dbBar.BarId,
                    Name = dbBar.Name,
                    Address = dbBar.Address
                };

                foreach (var dbBeer in dbBar.Beers)
                {
                    var brewery = new BreweryViewModel
                    {
                        BreweryId = dbBeer.Brewery.BreweryId,
                        Name = dbBeer.Brewery.Name
                    };

                    var beer = new BeerViewModel
                    {
                        BeerId = dbBeer.BeerId,
                        Name = dbBeer.Name,
                        PercentageAlcoholByVolume = dbBeer.PercentageAlcoholByVolume,
                        BreweryId = brewery.BreweryId
                    };

                    brewery.Beers.Add(beer);
                    bar.Beers.Add(beer);
                }

                bars.Add(bar);
            }

            return bars;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BarViewModel>> GetBar(int id)
        {
            var dbBar = await _repository.GetBar(id);

            var beers = new List<BeerViewModel>();
            foreach(var dbBeer in dbBar.Beers)
            {
                var beer = new BeerViewModel
                {
                    BeerId = dbBeer.BeerId,
                    BreweryId = dbBeer.Brewery.BreweryId,
                    Name = dbBeer.Brewery.Name,
                    PercentageAlcoholByVolume = dbBeer.PercentageAlcoholByVolume
                };

                beers.Add(beer);
            }

            var bar = new BarViewModel
            {
                BarId = dbBar.BarId,
                Name = dbBar.Name,
                Address = dbBar.Address,
                Beers = beers
            };

            return bar;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBar(int id, BarViewModel bar)
        {
            if (id != bar.BarId)
            {
                return BadRequest();
            }

            var dbBar = await _repository.GetBar(id);

            dbBar.Address = bar.Address;
            dbBar.Name = bar.Name;

            var result = await _repository.UpdateBar(dbBar);

            if(result == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<BarViewModel>> PostBar(BarViewModel bar)
        {
            var dbBar = new Bar
            {
                BarId = bar.BarId,
                Name = bar.Name,
                Address = bar.Address
            };

            int barId = await _repository.AddBar(dbBar);

            return CreatedAtAction(nameof(GetBar), new { id = barId }, bar);
        }

        [HttpGet("beer")]
        public async Task<ActionResult<List<BarViewModel>>> GetBarsWithBeers()
        {
            var dbBars = await _repository.GetAllBars();

            var barViewModels = new List<BarViewModel>();
            foreach(var dbBar in dbBars)
            {
                var bar = new BarViewModel
                {
                    BarId = dbBar.BarId,
                    Address = dbBar.Address,
                    Name = dbBar.Name
                };

                foreach(var dbBeer in dbBar.Beers)
                {
                    var brewery = new BreweryViewModel
                    {
                        BreweryId = dbBeer.Brewery.BreweryId,
                        Name = dbBeer.Brewery.Name
                    };

                    var beer = new BeerViewModel
                    {
                        BeerId = dbBeer.BeerId,
                        Name = dbBeer.Name,
                        PercentageAlcoholByVolume = dbBeer.PercentageAlcoholByVolume,
                        BreweryId = brewery.BreweryId
                    };

                    brewery.Beers.Add(beer);
                    bar.Beers.Add(beer);
                }

                barViewModels.Add(bar);
            }

            return barViewModels;
        }

        [HttpGet("{barId}/beer")]
        public async Task<ActionResult<BarViewModel>> GetBarWithBeers(int barId)
        {
            var dbBar = await _repository.GetBar(barId);

            var barViewModel = new BarViewModel
            {
                BarId = dbBar.BarId,
                Address = dbBar.Address,
                Name = dbBar.Name
            };

            foreach (var dbBeer in dbBar.Beers)
            {
                var brewery = new BreweryViewModel
                {
                    BreweryId = dbBeer.Brewery.BreweryId,
                    Name = dbBeer.Brewery.Name
                };

                var beer = new BeerViewModel
                {
                    BeerId = dbBeer.BeerId,
                    Name = dbBeer.Name,
                    PercentageAlcoholByVolume = dbBeer.PercentageAlcoholByVolume,
                    BreweryId = brewery.BreweryId
                };

                brewery.Beers.Add(beer);
                barViewModel.Beers.Add(beer);
            }

            return barViewModel;
        }
    }
}
