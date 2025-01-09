using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PetCare.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // Kontroler do zarządzania wizytami
    public class VisitsController : ControllerBase
    {
        private readonly AppDbContext _context;

        // Konstruktor kontrolera wizyt
        public VisitsController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Pobiera wszystkie wizyty.
        /// </summary>
        /// <returns>Lista wizyt.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllVisits()
        {
            var visits = await _context.Visits.ToListAsync();
            return Ok(visits);
        }

        /// <summary>
        /// Dodaje nową wizytę.
        /// </summary>
        /// <param name="visitCreateDto">Wizyta do dodania.</param>
        /// <returns>Utworzona wizyta.</returns>
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

        /// <summary>
        /// Edytuje istniejącą wizytę.
        /// </summary>
        /// <param name="id">ID wizyty do edycji.</param>
        /// <param name="visitCreateDto">Zaktualizowane dane wizyty.</param>
        /// <returns>Brak zawartości.</returns>
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

        /// <summary>
        /// Usuwa wizytę.
        /// </summary>
        /// <param name="id">ID wizyty do usunięcia.</param>
        /// <returns>Brak zawartości.</returns>
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
