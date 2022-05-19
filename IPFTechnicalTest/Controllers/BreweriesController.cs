using Microsoft.AspNetCore.Mvc;

using IPFTechnicalTest.Models;
using IPFTechnicalTest.Repository;

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

        // GET: api/Breweries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brewery>>> GetBrewery()
        {
            return await _repository.GetAllBreweries();
        }

        // GET: api/Breweries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Brewery>> GetBrewery(int id)
        {
            return await _repository.GetBrewery(id);
        }

        // PUT: api/Breweries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrewery(int id, Brewery brewery)
        {
            if (id != brewery.BreweryId)
            {
                return BadRequest();
            }

            var result = await _repository.UpdateBrewery(brewery);

            if (result == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Breweries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Brewery>> PostBrewery(Brewery brewery)
        {
            int breweryId = await _repository.AddBrewery(brewery);

            return CreatedAtAction(nameof(PostBrewery), new { id = breweryId }, brewery);
        }

        [HttpGet("beer")]
        public async Task<ActionResult<IEnumerable<Brewery>>> GetBreweriesWithBeers()
        {
            return await _repository.GetAllBreweries();
        }

        [HttpGet("{breweryId}/beer")]
        public async Task<ActionResult<Brewery>> GetBreweryWithBeers(int breweryId)
        {
            return await _repository.GetBrewery(breweryId);
        }

        [HttpPost("beer")]
        public async Task<ActionResult<int>> PostBreweryBeer(int breweryId, int beerId)
        {
            return await _repository.AddBreweryBeer(breweryId, beerId);
        }

        // DELETE: api/Breweries/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteBrewery(int id)
        //{
        //    var result = await _repository.DeleteBrewery(id);

        //    if (!result)
        //    {
        //        return NotFound();
        //    }

        //    return NoContent();
        //}
    }
}
