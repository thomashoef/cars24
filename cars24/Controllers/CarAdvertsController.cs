using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cars24.Models;
using cars24.Helpers;
using System.Linq;
using cars24.Enumerations;

namespace cars24.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarAdvertsController : ControllerBase
    {
        private readonly CarAdvertContext _context;

        // TODO remove once demo inserts are removed
        private static bool hasInitializedDbWithDemoData = false;

        public CarAdvertsController(CarAdvertContext context)
        {
            _context = context;

            // TODO Remove these hardcoded lines if you like, left them here to pre-populate the in-memory db
            if (!hasInitializedDbWithDemoData) {
                hasInitializedDbWithDemoData = true;
                _context.CarAdverts.Add(new CarAdvert() { Id = 1, Fuel = FuelType.Diesel, IsNew = false, Mileage = 10000, FirstRegistration = DateTime.Now.Date, Price = 10000, Title = "First test car" });
                _context.CarAdverts.Add(new CarAdvert() { Id = 2, Fuel = FuelType.Gasoline, IsNew = false, Mileage = 20000, FirstRegistration = DateTime.Now.Date, Price = 8000, Title = "Second test car" });
                _context.CarAdverts.Add(new CarAdvert() { Id = 3, Fuel = FuelType.Gasoline, IsNew = true, Mileage = 0, Price = 6000, Title = "Third test car" });
                _context.CarAdverts.Add(new CarAdvert() { Id = 4, Fuel = FuelType.Diesel, IsNew = true, Mileage = 0, Price = 36000, Title = "Fourth test car" });
                _context.SaveChanges();
            }
        }
                
        // GET: api/CarAdverts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarAdvert>>> GetCarAdverts([FromQuery] string sortBy = null)
        {
            return await _context.CarAdverts.OrderBy(SortHelper.CarAdvert(sortBy)).ToListAsync();
        }

        // GET: api/CarAdverts/5
        [HttpGet("{id:int}")]
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
