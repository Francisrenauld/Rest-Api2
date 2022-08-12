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
    public class CustomerController : ControllerBase
    {
        private readonly RocketElevatorsContext _context;

        public CustomerController(RocketElevatorsContext context)
        {
            _context = context;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Getcustomers()
        {
          if (_context.customers == null)
          {
              return NotFound();
          }
            return await _context.customers.ToListAsync();
        }

        [HttpGet("/api/Customer_By_Email/{Email_Of_The_Company_Contact}")]
        public async Task<ActionResult<Customer>> GetCustomerByEmail(string Email_Of_The_Company_Contact)
        {
          if (_context.customers == null)
          {
              return NotFound();
          }
            var customer = await _context.customers.Where(x => x.Email_Of_The_Company_Contact == Email_Of_The_Company_Contact).FirstOrDefaultAsync();

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // GET: api/Customer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(long id)
        {
          if (_context.customers == null)
          {
              return NotFound();
          }
            var customer = await _context.customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        [HttpGet("/api/email/{Email_Of_The_Company_Contact}")]
        public async Task<ActionResult<Customer>> GetCustomerEmail(string Email_Of_The_Company_Contact)
        {
          if (_context.customers == null)
          {
              return NotFound();
          }
           
            var customer = await _context.customers.Where(x => x.Email_Of_The_Company_Contact == Email_Of_The_Company_Contact).Include(b => b.Buildings).ThenInclude(bu => bu.Batteries).ThenInclude(c => c.Columns).ThenInclude(e => e.Elevators).ToListAsync();

            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT: api/Customer/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(long id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customer
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
          if (_context.customers == null)
          {
              return Problem("Entity set 'RocketElevatorsContext.customers'  is null.");
          }
            _context.customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(long id)
        {
            if (_context.customers == null)
            {
                return NotFound();
            }
            var customer = await _context.customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(long id)
        {
            return (_context.customers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
