using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using RawPace.Data;
using RawPace.Models;

namespace RawPace.Controllers
{
    [Authorize] // <--- This locks down the entire controller to only logged-in scouts
    [Route("api/[controller]")]
    [ApiController]
    public class BowlersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BowlersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Helper method to extract the logged-in scout's ID from their JWT token
        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(userIdClaim ?? "0");
        }

        [HttpGet]
        public IActionResult GetBowlers()
        {
            int userId = GetCurrentUserId();
           
            return Ok(_context.Bowlers.Where(b => b.UserId == userId).ToList());
        }

        [HttpPost]
        public IActionResult AddBowler([FromBody] Bowler bowler)
        {
            bowler.UserId = GetCurrentUserId(); 
            _context.Bowlers.Add(bowler);
            _context.SaveChanges();

            var initialSpeed = new SpeedRecord
            {
                BowlerId = bowler.Id,
                TopSpeed = bowler.TopSpeed,
                DateRecorded = DateTime.UtcNow
            };
            _context.SpeedRecords.Add(initialSpeed);
            _context.SaveChanges();

            return Ok(bowler);

            
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBowler(int id, [FromBody] Bowler updatedBowler)
        {
            int userId = GetCurrentUserId();

            var existingBowler = _context.Bowlers.FirstOrDefault(b => b.Id == id && b.UserId == userId);
            if (existingBowler == null) return NotFound();

            if (existingBowler.Status != updatedBowler.Status)
            {
                var logEntry = new InjuryRecord
                {
                    BowlerId = existingBowler.Id,
                    Status = updatedBowler.Status,
                    DateRecorded = DateTime.UtcNow,
                    Notes = $"Status automatically updated to {updatedBowler.Status}"
                };
                _context.InjuryRecords.Add(logEntry);
            }

           
            if (existingBowler.TopSpeed != updatedBowler.TopSpeed)
            {
                var speedLog = new SpeedRecord
                {
                    BowlerId = existingBowler.Id,
                    TopSpeed = updatedBowler.TopSpeed,
                    DateRecorded = DateTime.UtcNow
                };
                _context.SpeedRecords.Add(speedLog);
            }

            
            existingBowler.Name = updatedBowler.Name;
            existingBowler.TopSpeed = updatedBowler.TopSpeed;
            existingBowler.AvgSpeed = updatedBowler.AvgSpeed;
            existingBowler.Status = updatedBowler.Status;
            existingBowler.OversBowled = updatedBowler.OversBowled;
            existingBowler.Specialty = updatedBowler.Specialty;

            _context.SaveChanges();
            return Ok(existingBowler);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBowler(int id)
        {
            int userId = GetCurrentUserId();
            var bowler = _context.Bowlers.FirstOrDefault(b => b.Id == id && b.UserId == userId);
            if (bowler == null) return NotFound();

            _context.Bowlers.Remove(bowler);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet("{id}/history")]
        public IActionResult GetInjuryHistory(int id)
        {
            var history = _context.InjuryRecords
                                  .Where(ir => ir.BowlerId == id)
                                  .OrderByDescending(ir => ir.DateRecorded)
                                  .ToList();
            return Ok(history);
        }


        [HttpGet("{id}/speed-history")]
        public IActionResult GetSpeedHistory(int id)
        {
            
            var history = _context.SpeedRecords
                                  .Where(sr => sr.BowlerId == id)
                                  .OrderBy(sr => sr.DateRecorded)
                                  .ToList();
            return Ok(history);
        }
    }


}