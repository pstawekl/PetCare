using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
// Kontroler do zarządzania zwierzętami
public class PetsController : ControllerBase
{
    private readonly IPetRepository _petRepository;
    private readonly ILogger<PetsController> _logger;
    private readonly AppDbContext _context;

    public PetsController(IPetRepository petRepository, ILogger<PetsController> logger, AppDbContext context)
    {
        _petRepository = petRepository;
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pet>>> GetPets()
    {
        _logger.LogInformation("Getting all pets at {Time}", DateTime.Now);
        try
        {
            var pets = await _petRepository.GetAllAsync();
            return Ok(pets);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting pets");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Pet>> GetPet(int id)
    {
        _logger.LogInformation("Getting pet with id {Id} at {Time}", id, DateTime.Now);
        try
        {
            var pet = await _petRepository.GetByIdAsync(id);
            if (pet == null)
                return NotFound();
                
            return Ok(pet);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while getting pet with id {Id}", id);
            return StatusCode(500, "Internal server error");
        }
    }

    // Dodaj nowe zwierzę
    [HttpPost]
    public async Task<IActionResult> AddPet([FromBody] PetCreateDto petCreateDto)
    {
        var pet = new Pet
        {
            Name = petCreateDto.Name,
            Type = petCreateDto.Type,
            BirthDate = petCreateDto.BirthDate,
            Owner = petCreateDto.Owner
        };
        await _petRepository.AddAsync(pet);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPets), new { id = pet.Id }, pet);
    }

    // Zaktualizuj istniejące zwierzę
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePet(int id, [FromBody] PetCreateDto petCreateDto)
    {
        var existingPet = await _petRepository.GetByIdAsync(id);
        if (existingPet == null) return NotFound();

        existingPet.Name = petCreateDto.Name;
        existingPet.Type = petCreateDto.Type;
        existingPet.BirthDate = petCreateDto.BirthDate;
        existingPet.Owner = petCreateDto.Owner;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    // Usuń zwierzę
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePet(int id)
    {
        var pet = await _petRepository.GetByIdAsync(id);
        if (pet == null) return NotFound();
        _petRepository.DeleteAsync(pet.Id);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
