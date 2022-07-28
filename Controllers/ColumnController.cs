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
    public class ColumnController : ControllerBase
    {
        private readonly RocketElevatorsContext _context;

        public ColumnController(RocketElevatorsContext context)
        {
            _context = context;
        }

        // GET: api/Column
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Column>>> Getcolumns()
        {
          if (_context.columns == null)
          {
              return NotFound();
          }
            return await _context.columns.ToListAsync();
        }

        // GET: api/Column/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Column>> GetColumn(long id)
        {
          if (_context.columns == null)
          {
              return NotFound();
          }
            var column = await _context.columns.FindAsync(id);

            if (column == null)
            {
                return NotFound();
            }

            return Ok(column.Status);
        }

        // PUT: api/Column/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}/{status}")]
        public async Task<IActionResult> PutColumn(long id,  string status)
        {
            var entity = _context.columns.FirstOrDefault(column => column.Id == id);

            if (entity == null)
            {
                return NotFound();
            }
          
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
                if (!ColumnExists(id))
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

        // POST: api/Column
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Column>> PostColumn(Column column)
        {
          if (_context.columns == null)
          {
              return Problem("Entity set 'RocketElevatorsContext.columns'  is null.");
          }
            _context.columns.Add(column);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetColumn", new { id = column.Id }, column);
        }

        // DELETE: api/Column/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteColumn(long id)
        {
            if (_context.columns == null)
            {
                return NotFound();
            }
            var column = await _context.columns.FindAsync(id);
            if (column == null)
            {
                return NotFound();
            }

            _context.columns.Remove(column);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ColumnExists(long id)
        {
            return (_context.columns?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
