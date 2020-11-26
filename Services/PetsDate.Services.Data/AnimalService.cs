namespace PetsDate.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
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

        public IEnumerable<AnimalListAllViewModel> GetAll(int page, int itemsPerPage = 12)
        {
            return this.animalsRepository.AllAsNoTracking()
                .OrderBy(x => x.Id)
                .Skip(page * itemsPerPage).Take(itemsPerPage)
                .Select(x => new AnimalListAllViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Age = x.Age,
                    Color = x.Color,
                    Weight = x.Weight,
                    CategoryName = x.Category.Name,
                    CategoryId = x.CategoryId,
                    ImageUrl = "/AnimalImages/",
                }).ToList();
            //// 1-12 - page 1  skip 0  (page - 1) * itemsPerPage
            // 13-24 - page 2  skip 12
        }
    }
}
