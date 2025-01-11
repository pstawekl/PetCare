public interface IDogApiService
{
    Task<IEnumerable<DogBreed>> GetAllBreedsAsync();
    Task<DogBreed> GetBreedByNameAsync(string breedName);
}