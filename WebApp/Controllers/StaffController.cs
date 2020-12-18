using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly kindergartenContext _context;

        public StaffController(kindergartenContext context)
        {
            _context = context;
        }

        // GET: api/Staff
        [HttpGet]
        public List<Staff> GetStaffs()
        {
            var staff = _context.Staff.Include(x => x.Position).Take(20);
            return staff.ToList();
        }
        [HttpGet("positions")]
        public IEnumerable<Position> GetPositions()
        {
            return _context.Positions.ToList();
        }

        // GET: api/Staff/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Staff>> GetStaff(int id)
        {
            var staff = await _context.Staff.FindAsync(id);

            if (staff == null)
            {
                return NotFound();
            }

            return staff;
        }

        // PUT: api/Staff/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public IActionResult Put([FromBody] Staff staff)
        {
            if (staff == null)
            {
                return BadRequest();
            }
            if (!_context.Staff.Any(x => x.Id == staff.Id))
            {
                return NotFound();
            }

            _context.Update(staff);
            _context.SaveChanges();

            staff.Position = _context.Positions.FirstOrDefault(x => x.Id == staff.PositionId);

            return Ok(staff);
        }

        // POST: api/Staff
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public IActionResult Post([FromBody] Staff staff)
        {
            if (staff == null)
            {
                return BadRequest();
            }

            _context.Staff.Add(staff);
            _context.SaveChanges();
            return Ok(staff);
        }

        // DELETE: api/Staff/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            var staff = await _context.Staff.FindAsync(id);
            if (staff == null)
            {
                return NotFound();
            }

            _context.Staff.Remove(staff);
            await _context.SaveChangesAsync();

            return Ok(staff);
        }

        private bool StaffExists(int id)
        {
            return _context.Staff.Any(e => e.Id == id);
        }
    }
}
