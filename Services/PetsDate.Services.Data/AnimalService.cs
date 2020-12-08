namespace PetsDate.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Identity;
    using PetsDate.Data.Common.Repositories;
    using PetsDate.Data.Models;
    using PetsDate.Web.ViewModels.Animal;

    public class AnimalService : IAnimalService
    {
        private readonly IRepository<AnimalImage> animalImagesRepository;
        private readonly IDeletableEntityRepository<Animal> animalsRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly Cloudinary cloudinary;
        private readonly ICloudinaryService cloudinaryService;

        public AnimalService(
            IRepository<AnimalImage> animalImagesRepository,
            IDeletableEntityRepository<Animal> animalsRepository,
            UserManager<ApplicationUser> userManager,
            Cloudinary cloudinary,
            ICloudinaryService cloudinaryService)
        {
            this.animalImagesRepository = animalImagesRepository;
            this.animalsRepository = animalsRepository;
            this.userManager = userManager;
            this.cloudinary = cloudinary;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task CreateAsync(CreateAnimalInputModel input, string userId)
        {
            var imageUrl = await this.cloudinaryService.UploadAsync(this.cloudinary, input.Image);

            var animal = new Animal
            {
                UserId = userId,
                CategoryId = input.CategoryId,
                Name = input.Name,
                Age = input.Age,
                Color = input.Color,
                Weight = input.Weight,
                ImageUrl = imageUrl,
            };

            await this.animalsRepository.AddAsync(animal);
            await this.animalsRepository.SaveChangesAsync();
        }

        public async Task CreateAnimalImageAsync(AddImagesInputModel input, string userId, int animalId)
        {
            foreach (var image in input.Images)
            {
                var imageUrl = await this.cloudinaryService.UploadAsync(this.cloudinary, image);

                var tempImage = new AnimalImage
                {
                    UserId = userId,
                    AnimalId = animalId,
                    ImageUrl = imageUrl,
                };

                await this.animalImagesRepository.AddAsync(tempImage);
            }

            await this.animalImagesRepository.SaveChangesAsync();
        }

        public IEnumerable<AnimalListAllViewModel> GetAll(int page, string userId, int itemsPerPage = 6)
        {
            return this.animalsRepository.AllAsNoTracking()
                .Where(x => x.User.Id == userId)
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new AnimalListAllViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Age = x.Age,
                    Color = x.Color,
                    Weight = x.Weight,
                    CategoryName = x.Category.Name,
                    CategoryId = x.CategoryId,
                    ImageUrl = x.ImageUrl,
                }).ToList();
            //// 1-12 - page 1  skip 0  (page - 1) * itemsPerPage
            // 13-24 - page 2  skip 12
        }

        public int GetImagesCount()
        {
            return this.animalImagesRepository.All().Count();
        }

        public int GetCount()
        {
            return this.animalsRepository.All().Count();
        }

        public IEnumerable<AnimalImageListAllViewModel> GetAnimalImages(string userId, int animalId)
        {
            return this.animalImagesRepository.AllAsNoTracking()
                .Where(x => x.UserId == userId && x.AnimalId == animalId)
                .Select(x => new AnimalImageListAllViewModel
                {
                   ImageUrl = x.ImageUrl,
                }).ToList();
        }
    }
}
