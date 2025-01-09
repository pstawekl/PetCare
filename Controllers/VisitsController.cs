using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PetCare.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VisitsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VisitsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVisits()
        {
            var visits = await _context.Visits.ToListAsync();
            return Ok(visits);
        }

        [HttpPost]
        public async Task<IActionResult> AddVisit([FromBody] VisitCreateDto visitCreateDto)
        {
            var visit = new Visit
            {
                PetId = visitCreateDto.PetId,
                VisitDate = visitCreateDto.VisitDate,
                Description = visitCreateDto.Description,
                IsCompleted = visitCreateDto.IsCompleted
            };
            await _context.Visits.AddAsync(visit);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAllVisits), new { id = visit.Id }, visit);
        }

        [HttpPut]
        public async Task<IActionResult> EditVisit(int id, [FromBody] VisitCreateDto visitCreateDto)
        {
            var existingVisit = await _context.Visits.FindAsync(id);
            if (existingVisit == null) return NotFound();

            var visit = new Visit
            {
                Id = id,
                PetId = visitCreateDto.PetId,
                VisitDate = visitCreateDto.VisitDate,
                Description = visitCreateDto.Description,
                IsCompleted = visitCreateDto.IsCompleted
            };

            existingVisit.PetId = visit.PetId;
            existingVisit.VisitDate = visit.VisitDate;
            existingVisit.Description = visit.Description;
            existingVisit.IsCompleted = visit.IsCompleted;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVisit(int id)
        {
            var visit = await _context.Visits.FindAsync(id);
            if (visit == null) return NotFound();
            _context.Visits.Remove(visit);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
