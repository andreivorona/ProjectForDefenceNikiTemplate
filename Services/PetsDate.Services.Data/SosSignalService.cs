namespace PetsDate.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using PetsDate.Data.Common.Repositories;
    using PetsDate.Data.Models;
    using PetsDate.Web.ViewModels.SosSignal;

    public class SosSignalService : ISosSignalService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<SosSignal> sosSignalsRepository;
        private readonly Cloudinary cloudinary;
        private readonly ICloudinaryService cloudinaryService;

        public SosSignalService(
            IDeletableEntityRepository<SosSignal> sosSignalsRepository,
            Cloudinary cloudinary,
            ICloudinaryService cloudinaryService)
        {
            this.sosSignalsRepository = sosSignalsRepository;
            this.cloudinary = cloudinary;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task CreateAsync(CreateSosSignalInputModel input, string userId)
        {
            var extension = Path.GetExtension(input.Image.FileName);

            if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            {
                throw new Exception($"Invalid image extension {extension}");
            }

            var imageUrl = await this.cloudinaryService.UploadAsync(this.cloudinary, input.Image);

            var sosSignal = new SosSignal
            {
                UserId = userId,
                Name = input.Name,
                Location = input.Location,
                Description = input.Description,
                ImageUrl = imageUrl,
            };

            await this.sosSignalsRepository.AddAsync(sosSignal);
            await this.sosSignalsRepository.SaveChangesAsync();
        }

        public IEnumerable<SosSignalListAllViewModel> GetAll(int page, string userId, int itemsPerPage)
        {
            return this.sosSignalsRepository.AllAsNoTracking()
                .Where(x => x.User.Id == userId)
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new SosSignalListAllViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Location = x.Location,
                    ImageUrl = x.ImageUrl,
                }).ToList();
        }

        public IEnumerable<SosSignalListAllViewModel> GetAllHomePage()
        {
            return this.sosSignalsRepository.AllAsNoTracking()
                .Select(x => new SosSignalListAllViewModel
                {
                    Name = x.Name,
                    Description = x.Description,
                    Location = x.Location,
                    ImageUrl = x.ImageUrl,
                    UserName = x.User.UserName,
                }).ToList();
        }

        public SosSignalListAllViewModel GetInfo(string userId, string sosSignalId)
        {
            return this.sosSignalsRepository.AllAsNoTracking()
                .Where(x => x.Id == sosSignalId && x.UserId == userId)
                .Select(x => new SosSignalListAllViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Location = x.Location,
                    ImageUrl = x.ImageUrl,
                    UserName = x.User.UserName,
                }).FirstOrDefault();
        }

        public int GetCount()
        {
            return this.sosSignalsRepository.All().Count();
        }
    }
}
