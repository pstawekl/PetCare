using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

[ApiController]
[Route("api/[controller]")]
public class PetsController : ControllerBase
{
    private readonly AppDbContext _context;

    public PetsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPets()
    {
        var pets = await _context.Pets.ToListAsync();
        return Ok(pets);
    }

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
        await _context.Pets.AddAsync(pet);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetAllPets), new { id = pet.Id }, pet);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePet(int id, [FromBody] PetCreateDto petCreateDto)
    {
        var existingPet = await _context.Pets.FindAsync(id);
        if (existingPet == null) return NotFound();

        var pet = new Pet
        {
            Id = id,
            Name = petCreateDto.Name,
            Type = petCreateDto.Type,
            BirthDate = petCreateDto.BirthDate,
            Owner = petCreateDto.Owner
        };

        existingPet.Name = pet.Name;
        existingPet.Type = pet.Type;
        existingPet.BirthDate = pet.BirthDate;
        existingPet.Owner = pet.Owner;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePet(int id)
    {
        var pet = await _context.Pets.FindAsync(id);
        if (pet == null) return NotFound();
        _context.Pets.Remove(pet);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
