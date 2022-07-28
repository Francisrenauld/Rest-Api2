using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RocketElevators.Models;

namespace RocketElevators.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElevatorController : ControllerBase
    {
        private readonly RocketElevatorsContext _context;

        public ElevatorController(RocketElevatorsContext context)
        {
            _context = context;
        }

        // GET: api/Elevator
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Elevator>>> Getelevators()
        {
            if (_context.elevators == null)
            {
                return NotFound();
            }
            return await _context.elevators.ToListAsync();
        }

        // GET: api/Elevator/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Elevator>> GetElevator(long id)
        {
            if (_context.elevators == null)
            {
                return NotFound();
            }
            var elevator = await _context.elevators.FindAsync(id);

            if (elevator == null)
            {
                return NotFound();
            }

            return Ok(elevator.Status);
        }

        // GET: /api/ElevatorList/NotInOperation
        [HttpGet("/api/ElevatorList/NotInOperation")]
        public async Task<ActionResult<IEnumerable<Elevator>>> GetElevatorsNotInOperation()
        {
            
            if (_context.elevators == null)
            {
                return NotFound();
            }
            return await _context.elevators.Where(elevator => elevator.Status != "Active").ToListAsync();
        }

        // PUT: api/Elevator/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/{status}")]
        public async Task<IActionResult> PutElevator(long id, string status)
        {
            var entity = _context.elevators.FirstOrDefault(elevator => elevator.Id == id);

            if (entity == null)
            {
                return NotFound();
            }

            if (entity != null && status == "Active" || status == "Inactive" || status == "Intervention")
            {
                entity.Status = status;
                _context.SaveChanges();
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElevatorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(entity.Status);
        }

        // POST: api/Elevator
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Elevator>> PostElevator(Elevator elevator)
        {
            if (_context.elevators == null)
            {
                return Problem("Entity set 'RocketElevatorsContext.elevators'  is null.");
            }
            _context.elevators.Add(elevator);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetElevator", new { id = elevator.Id }, elevator);
        }

        // DELETE: api/Elevator/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteElevator(long id)
        {
            if (_context.elevators == null)
            {
                return NotFound();
            }
            var elevator = await _context.elevators.FindAsync(id);
            if (elevator == null)
            {
                return NotFound();
            }

            _context.elevators.Remove(elevator);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ElevatorExists(long id)
        {
            return (_context.elevators?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
