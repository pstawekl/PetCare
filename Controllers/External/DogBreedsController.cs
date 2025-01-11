using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class DogBreedsController : ControllerBase
{
    private readonly IDogApiService _dogApiService;
    private readonly ILogger<DogBreedsController> _logger;

    public DogBreedsController(IDogApiService dogApiService, ILogger<DogBreedsController> logger)
    {
        _dogApiService = dogApiService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<DogBreed>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<DogBreed>>> GetAllBreeds()
    {
        try
        {
            _logger.LogInformation("Getting all dog breeds");
            var breeds = await _dogApiService.GetAllBreedsAsync();
            return Ok(breeds);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching dog breeds");
            return StatusCode(500, "Internal server error while fetching dog breeds");
        }
    }

    [HttpGet("{name}")]
    [ProducesResponseType(typeof(DogBreed), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<DogBreed>> GetBreedByName(string name)
    {
        try
        {
            _logger.LogInformation("Getting dog breed information for: {Name}", name);
            var breed = await _dogApiService.GetBreedByNameAsync(name);
            
            if (breed == null)
                return NotFound($"Breed '{name}' not found");

            return Ok(breed);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching dog breed {Name}", name);
            return StatusCode(500, "Internal server error while fetching dog breed");
        }
    }
}