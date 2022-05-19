using Microsoft.AspNetCore.Mvc;

using IPFTechnicalTest.Models;
using IPFTechnicalTest.Repository;

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

        // GET: api/Beers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Beer>> GetBeer(int id)
        {
            return await _repository.GetBeer(id);
        }


        // PUT: api/Beers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBeer(int id, Beer beer)
        {
            if (id != beer.BeerId)
            {
                return BadRequest();
            }

            var result = await _repository.UpdateBeer(beer);

            if (result == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Beers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost()]
        public async Task<ActionResult<Beer>> PostBeer(Beer beer)
        {
            int beerId = await _repository.AddBeer(beer);

            return CreatedAtAction(nameof(PostBeer), new { id = beerId }, beer);
        }

        // DELETE: api/Beers/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteBeer(int id)
        //{
        //    var result = await _repository.DeleteBeer(id);

        //    if (!result)
        //    {
        //        return NotFound();
        //    }

        //    return NoContent();
        //}

        // GET: api/Beers?gtAlcoholByVolume=&ltAcoloholByVolume
        [HttpGet("beer")]
        public async Task<ActionResult<IEnumerable<Beer>>> GetBeerByAlcoholVolumeRange(decimal? gtAlcoholByVolume, decimal? ltAlcoholByVolume)
        {
            var beersWithinRange = await _repository.GetBeerByAlcoholVolumeRange(gtAlcoholByVolume, ltAlcoholByVolume);

            if (beersWithinRange == null)
            {
                return NotFound();
            }

            return beersWithinRange;
        }
    }
}
