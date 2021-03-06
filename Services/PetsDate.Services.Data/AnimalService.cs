﻿namespace PetsDate.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Identity;
    using PetsDate.Data.Common.Repositories;
    using PetsDate.Data.Models;
    using PetsDate.Web.ViewModels.Animal;

    public class AnimalService : IAnimalService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IRepository<AnimalImage> animalImagesRepository;
        private readonly IDeletableEntityRepository<Animal> animalsRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly Cloudinary cloudinary;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IVotesService votesService;

        public AnimalService(
            IRepository<AnimalImage> animalImagesRepository,
            IDeletableEntityRepository<Animal> animalsRepository,
            UserManager<ApplicationUser> userManager,
            Cloudinary cloudinary,
            ICloudinaryService cloudinaryService,
            IVotesService votesService)
        {
            this.animalImagesRepository = animalImagesRepository;
            this.animalsRepository = animalsRepository;
            this.userManager = userManager;
            this.cloudinary = cloudinary;
            this.cloudinaryService = cloudinaryService;
            this.votesService = votesService;
        }

        public async Task CreateAsync(CreateAnimalInputModel input, string userId)
        {
            var extension = Path.GetExtension(input.Image.FileName);

            if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            {
                throw new Exception($"Invalid image extension {extension}");
            }

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
                var extension = Path.GetExtension(image.FileName);

                if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension {extension}");
                }

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

        public IEnumerable<AnimalListAllViewModel> GetAllHomePage()
        {
            return this.animalsRepository.AllAsNoTracking()
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
                    UserName = x.User.UserName,
                }).ToList();
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
                   Id = x.Id,
                   ImageUrl = x.ImageUrl,
                }).ToList();
        }

        public IEnumerable<AnimalImageListAllViewModel> GetAnimalImagesHomePage(int animalId)
        {
            return this.animalImagesRepository.AllAsNoTracking()
                .Where(x => x.AnimalId == animalId)
                .Select(x => new AnimalImageListAllViewModel
                {
                    ImageUrl = x.ImageUrl,
                }).ToList();
        }

        public AnimalListAllViewModel GetInfoHomePage(int animalId)
        {
            var votesAverageValue = this.votesService.GetAverageVotes(animalId);

            var result = this.animalsRepository.AllAsNoTracking()
                .Where(x => x.Id == animalId)
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
                    VotesAverageValue = votesAverageValue,
                }).FirstOrDefault();

            return result;
        }

        public AnimalListAllViewModel GetInfo(string userId, int animalId)
        {
            var votesAverageValue = this.votesService.GetAverageVotes(animalId);

            var result = this.animalsRepository.AllAsNoTracking()
                .Where(x => x.Id == animalId && x.UserId == userId)
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
                    VotesAverageValue = votesAverageValue,
                }).FirstOrDefault();

            return result;
        }

        public EditAnimalInputModel GetById(int animalId)
        {
            var result = this.animalsRepository.AllAsNoTracking()
                .Where(x => x.Id == animalId)
                .Select(x => new EditAnimalInputModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Age = x.Age,
                    Color = x.Color,
                    Weight = x.Weight,
                    CategoryId = x.CategoryId,
                    ImageUrl = x.ImageUrl,
                }).FirstOrDefault();

            return result;
        }

        public async Task UpdateAsync(int animalId, EditAnimalInputModel input)
        {
            var animal = this.animalsRepository.All()
                .FirstOrDefault(x => x.Id == animalId);

            animal.Name = input.Name;
            animal.Age = input.Age;
            animal.ImageUrl = input.ImageUrl;
            animal.Weight = input.Weight;
            animal.Color = input.Color;
            animal.CategoryId = input.CategoryId;

            await this.animalsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int animalId)
        {
            var animal = this.animalsRepository.All()
                .FirstOrDefault(x => x.Id == animalId);

            this.animalsRepository.Delete(animal);
            await this.animalsRepository.SaveChangesAsync();
        }

        public async Task DeleteImageFromCollectionAsync(int id)
        {
            var element = this.animalImagesRepository.All()
                .FirstOrDefault(x => x.Id == id);

            this.animalImagesRepository.Delete(element);
            await this.animalImagesRepository.SaveChangesAsync();
        }
    }
}
