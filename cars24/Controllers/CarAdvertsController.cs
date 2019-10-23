using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cars24.Models;

namespace cars24.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarAdvertsController : ControllerBase
    {
        private readonly CarAdvertContext _context;

        public CarAdvertsController(CarAdvertContext context)
        {
            _context = context;
        }

        // GET: api/CarAdverts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarAdvert>>> GetCarAdverts()
        {
            return await _context.CarAdverts.ToListAsync();
        }

        // GET: api/CarAdverts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarAdvert>> GetCarAdvert(int id)
        {
            var carAdvert = await _context.CarAdverts.FindAsync(id);

            if (carAdvert == null)
            {
                return NotFound();
            }

            return carAdvert;
        }

        // PUT: api/CarAdverts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarAdvert(int id, CarAdvert carAdvert)
        {
            if (id != carAdvert.Id)
            {
                return BadRequest();
            }

            _context.Entry(carAdvert).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarAdvertExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/CarAdverts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<CarAdvert>> PostCarAdvert(CarAdvert carAdvert)
        {
            _context.CarAdverts.Add(carAdvert);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarAdvert", new { id = carAdvert.Id }, carAdvert);
        }

        // DELETE: api/CarAdverts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CarAdvert>> DeleteCarAdvert(int id)
        {
            var carAdvert = await _context.CarAdverts.FindAsync(id);
            if (carAdvert == null)
            {
                return NotFound();
            }

            _context.CarAdverts.Remove(carAdvert);
            await _context.SaveChangesAsync();

            return carAdvert;
        }

        private bool CarAdvertExists(int id)
        {
            return _context.CarAdverts.Any(e => e.Id == id);
        }
    }
}
