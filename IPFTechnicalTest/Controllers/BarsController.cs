﻿using Microsoft.AspNetCore.Mvc;

using IPFTechnicalTest.Models;
using IPFTechnicalTest.Repository;

namespace IPFTechnicalTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarsController : ControllerBase
    {
        private readonly IBeerRepository _repository;

        public BarsController(IBeerRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Bars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bar>>> GetBar()
        {
            return await _repository.GetAllBars();
        }

        //GET: api/Bars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bar>> GetBar(int id)
        {
            return await _repository.GetBar(id);
        }

        // PUT: api/Bars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBar(int id, Bar bar)
        {
            if (id != bar.BarId)
            {
                return BadRequest();
            }

            var result = await _repository.UpdateBar(bar);

            if(result == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Bars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bar>> PostBar(Bar bar)
        {
            int barId = await _repository.AddBar(bar);

            return CreatedAtAction(nameof(PostBar), new { id = barId }, bar);
        }

        // DELETE: api/Bars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBar(int id)
        {
            var result = await _repository.DeleteBar(id);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
