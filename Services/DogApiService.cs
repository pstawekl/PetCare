public class DogApiService : IDogApiService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<DogApiService> _logger;
    private readonly string _apiKey;

    public DogApiService(HttpClient httpClient, IConfiguration configuration, ILogger<DogApiService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _apiKey = configuration["ExternalApis:DogApi:ApiKey"];
        _httpClient.BaseAddress = new Uri("https://api.thedogapi.com/v1/");
        _httpClient.DefaultRequestHeaders.Add("x-api-key", _apiKey);
    }

    public async Task<IEnumerable<DogBreed>> GetAllBreedsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("breeds");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<DogBreed>>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching dog breeds from external API");
            throw;
        }
    }

    public async Task<DogBreed> GetBreedByNameAsync(string breedName)
    {
        try
        {
            var response = await _httpClient.GetAsync($"breeds/search?q={breedName}");
            response.EnsureSuccessStatusCode();
            var breeds = await response.Content.ReadFromJsonAsync<IEnumerable<DogBreed>>();
            return breeds.FirstOrDefault();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching dog breed {BreedName}", breedName);
            throw;
        }
    }
}