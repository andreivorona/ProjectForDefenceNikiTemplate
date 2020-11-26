namespace PetsDate.Services.Data
{
    using System.Threading.Tasks;

    using PetsDate.Data.Common.Repositories;
    using PetsDate.Data.Models;
    using PetsDate.Web.ViewModels.Animal;

    public class AnimalService : IAnimalService
    {
        private readonly IDeletableEntityRepository<Animal> animalsRepository;

        public AnimalService(
            IDeletableEntityRepository<Animal> animalsRepository)
        {
            this.animalsRepository = animalsRepository;
        }

        public async Task CreateAsync(CreateAnimalInputModel input, string userId)
        {
            var animal = new Animal
            {
                UserId = userId,
                CategoryId = input.CategoryId,
                Name = input.Name,
                Age = input.Age,
                Color = input.Color,
                Weight = input.Weight,
            };

            await this.animalsRepository.AddAsync(animal);
            await this.animalsRepository.SaveChangesAsync();
        }
    }
}
