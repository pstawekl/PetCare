using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetCare;

namespace PetCare.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // Kontroler do zarządzania wizytami
    public class VisitsController : ControllerBase
    {
        private readonly IVisitRepository _visitRepository;

        // Konstruktor kontrolera wizyt
        public VisitsController(IVisitRepository visitRepository)
        {
            _visitRepository = visitRepository;
        }

        /// <summary>
        /// Pobiera wszystkie wizyty.
        /// </summary>
        /// <returns>Lista wizyt.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllVisits()
        {
            var visits = await _visitRepository.GetAllAsync();
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
            await _visitRepository.AddAsync(visit);
            return CreatedAtAction(nameof(GetAllVisits), new { id = visit.Id }, visit);
        }

        /// <summary>
        /// Edytuje istniejącą wizytę.
        /// </summary>
        /// <param name="id">ID wizyty do edycji.</param>
        /// <param name="visitCreateDto">Zaktualizowane dane wizyty.</param>
        /// <returns>Brak zawartości.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> EditVisit(int id, [FromBody] VisitCreateDto visitCreateDto)
        {
            var existingVisit = await _visitRepository.GetByIdAsync(id);
            if (existingVisit == null) return NotFound();

            existingVisit.PetId = visitCreateDto.PetId;
            existingVisit.VisitDate = visitCreateDto.VisitDate;
            existingVisit.Description = visitCreateDto.Description;
            existingVisit.IsCompleted = visitCreateDto.IsCompleted;

            await _visitRepository.UpdateAsync(existingVisit);
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
            var visit = await _visitRepository.GetByIdAsync(id);
            if (visit == null) return NotFound();
            await _visitRepository.DeleteAsync(visit.Id);
            return NoContent();
        }
    }
}
