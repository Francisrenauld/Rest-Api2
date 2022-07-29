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
    public class InterventionController : ControllerBase
    {
        private readonly RocketElevatorsContext _context;

        public InterventionController(RocketElevatorsContext context)
        {
            _context = context;
        }

        // GET: api/Intervention
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Intervention>>> Getinterventions()
        {
          if (_context.interventions == null)
          {
            return NotFound();
          }
            return await _context.interventions.ToListAsync();
        }

        

         // GET: /api/InterventionPending
        [HttpGet("/api/InterventionPending")]
        public async Task<ActionResult<IEnumerable<Intervention>>> GetInterventionStatusPending()
        {
            
          if (_context.interventions == null)
          {
              return NotFound();
          }
            return await _context.interventions.Where(intervention => intervention.status =="Pending" && intervention.start_date_and_time_of_the_intervention == null).ToListAsync();
        }

        // GET: api/Intervention/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Intervention>> GetIntervention(long id)
        {
          if (_context.interventions == null)
          {
              return NotFound();
          }
            var intervention = await _context.interventions.FindAsync(id);

            if (intervention == null)
            {
                return NotFound();
            }
              if (intervention == null)
            {
                return BadRequest();
            }

            return intervention;
        }

        // PUT: api/Intervention/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIntervention(long id)
        {
             if (_context.interventions == null)
            {
                return BadRequest();
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterventionExists(id))
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

        // PUT: api/Intervention/5/StartIntervention
        [HttpPut("{id}/StartIntervention")]
        public async Task<IActionResult> StartIntervention(long id)
        {
            var interventionID = _context.interventions.FirstOrDefault(intervention => intervention.Id == id);

            if (_context.interventions == null)
            {
                return BadRequest();
            }else
            {
                interventionID.status = "InProgress";
                interventionID.start_date_and_time_of_the_intervention = DateTime.Now;

            }
        
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterventionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            if (interventionID == null)
            {
                return BadRequest();
            }
            return Ok(interventionID);
        }

        // PUT: api/Intervention/5/EndIntervention
        [HttpPut("{id}/EndIntervention")]
        public async Task<IActionResult> EndIntervention(long id)
        {
            var interventionID = _context.interventions.FirstOrDefault(intervention => intervention.Id == id);

            if (_context.interventions == null)
            {
                return BadRequest();
            } else
            {
               interventionID.status = "Completed";
                interventionID.end_date_and_time_of_the_intervention = DateTime.Now;

            }

          
        
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterventionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
             if (interventionID == null)
            {
                return BadRequest();
            }
            return Ok(interventionID);
        }

        // POST: api/Intervention
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Intervention>> PostIntervention(Intervention intervention)
        {
          if (_context.interventions == null)
          {
              return Problem("Entity set 'RocketElevatorsContext.interventions'  is null.");
          }
            _context.interventions.Add(intervention);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIntervention", new { id = intervention.Id }, intervention);
        }

        // DELETE: api/Intervention/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIntervention(long id)
        {
            if (_context.interventions == null)
            {
                return NotFound();
            }
            var intervention = await _context.interventions.FindAsync(id);
            if (intervention == null)
            {
                return NotFound();
            }

            _context.interventions.Remove(intervention);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InterventionExists(long id)
        {
            return (_context.interventions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
