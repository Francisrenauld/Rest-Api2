using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RocketElevators.Models;
using Newtonsoft.Json;


namespace RocketElevators.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private readonly RocketElevatorsContext _context;

        public BuildingController(RocketElevatorsContext context)
        {
            _context = context;
        }

        // GET: api/Building
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Building>>> Getbuildings()
        {
          if (_context.buildings == null)
          {
              return NotFound();
          }
            return await _context.buildings.ToListAsync();
        }

        // GET: api/Building/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Building>> GetBuilding(long id)
        {
          if (_context.buildings == null)
          {
              return NotFound();
          }
            var building = await _context.buildings.FindAsync(id);

            if (building == null)
            {
                return NotFound();
            }

            return building;
        }

                // GET: api/BuildingList
        [HttpGet("/api/BuildingsList")]
        public async Task<ActionResult<IEnumerable<Building>>> GetBuildingWithIntervention()
        {

          if (_context.buildings == null)
          {
              return NotFound();
          }
            return await _context.buildings.Where(b => b.Batteries.Any(bat => bat.Status == "Intervention"|| bat.Columns.Any(c => c.Status == "Intervention" || c.Elevators.Any(elev => elev.Status == "Intervention")))).Include(b => b.Batteries).ThenInclude(c => c.Columns).ThenInclude(e => e.Elevators).ToListAsync();
        }

        // PUT: api/Building/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuilding(long id, Building building)
        {
            if (id != building.Id)
            {
                return BadRequest();
            }

            _context.Entry(building).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuildingExists(id))
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

        // POST: api/Building
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Building>> PostBuilding(Building building)
        {
          if (_context.buildings == null)
          {
              return Problem("Entity set 'RocketElevatorsContext.buildings'  is null.");
          }
            _context.buildings.Add(building);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBuilding", new { id = building.Id }, building);
        }

        // DELETE: api/Building/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuilding(long id)
        {
            if (_context.buildings == null)
            {
                return NotFound();
            }
            var building = await _context.buildings.FindAsync(id);
            if (building == null)
            {
                return NotFound();
            }

            _context.buildings.Remove(building);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BuildingExists(long id)
        {
            return (_context.buildings?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
