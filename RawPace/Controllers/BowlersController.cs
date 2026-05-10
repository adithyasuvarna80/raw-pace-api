using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RawPace.Data;
using RawPace.Models;


namespace RawPace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BowlersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BowlersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bowler>>> GetBowlers()
        {

            return await _context.Bowlers.ToListAsync();
        }

        [HttpPost]

        public async Task<ActionResult<Bowler>> PostBowler(Bowler bowler)
        {
            _context.Bowlers.Add(bowler);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetBowlers), new { id = bowler.Id }, bowler);


        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBowler(int id, Bowler bowler)
        {
            if (id != bowler.Id)
            {
                return BadRequest("ID mismatch");
            }

            _context.Entry(bowler).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBowler(int id)
        {
            var bowler = await _context.Bowlers.FindAsync(id);
            if (bowler == null)
            {
                return NotFound();
            }

            _context.Bowlers.Remove(bowler);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
