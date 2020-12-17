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
    using PetsDate.Web.ViewModels.Clinic;

    public class ClinicService : IClinicService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Clinic> clinicsRepository;
        private readonly Cloudinary cloudinary;
        private readonly ICloudinaryService cloudinaryService;

        public ClinicService(
            IDeletableEntityRepository<Clinic> clinicsRepository,
            Cloudinary cloudinary,
            ICloudinaryService cloudinaryService)
        {
            this.clinicsRepository = clinicsRepository;
            this.cloudinary = cloudinary;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task CreateAsync(CreateClinicInputModel input, string userId)
        {
            var extension = Path.GetExtension(input.Image.FileName);

            if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            {
                throw new Exception($"Invalid image extension {extension}");
            }

            var imageUrl = await this.cloudinaryService.UploadAsync(this.cloudinary, input.Image);

            var clinic = new Clinic
            {
                UserId = userId,
                Name = input.Name,
                Location = input.Location,
                ImageUrl = imageUrl,
            };

            await this.clinicsRepository.AddAsync(clinic);
            await this.clinicsRepository.SaveChangesAsync();
        }

        public IEnumerable<ClinicListAllViewModel> GetAll(int page, string userId, int itemsPerPage)
        {
            return this.clinicsRepository.AllAsNoTracking()
                .Where(x => x.User.Id == userId)
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new ClinicListAllViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Location = x.Location,
                    ImageUrl = x.ImageUrl,
                }).ToList();
        }

        public IEnumerable<ClinicListAllViewModel> GetAllHomePage()
        {
            return this.clinicsRepository.AllAsNoTracking()
                .Select(x => new ClinicListAllViewModel
                {
                    Name = x.Name,
                    Location = x.Location,
                    ImageUrl = x.ImageUrl,
                    UserName = x.User.UserName,
                }).ToList();
        }

        public ClinicListAllViewModel GetInfo(string userId, string clinicId)
        {
            return this.clinicsRepository.AllAsNoTracking()
                .Where(x => x.Id == clinicId && x.UserId == userId)
                .Select(x => new ClinicListAllViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Location = x.Location,
                    ImageUrl = x.ImageUrl,
                    UserName = x.User.UserName,
                }).FirstOrDefault();
        }

        public EditClinicInputModel GetById(string clinicId)
        {
            var result = this.clinicsRepository.AllAsNoTracking()
                .Where(x => x.Id == clinicId)
                .Select(x => new EditClinicInputModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Location = x.Location,
                    ImageUrl = x.ImageUrl,
                }).FirstOrDefault();

            return result;
        }

        public async Task UpdateAsync(string clinicId, EditClinicInputModel input)
        {
            var clinic = this.clinicsRepository.All()
                .FirstOrDefault(x => x.Id == clinicId);

            clinic.Name = input.Name;
            clinic.ImageUrl = input.ImageUrl;
            clinic.Location = input.Location;

            await this.clinicsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string clinicId)
        {
            var clinic = this.clinicsRepository.All()
                .FirstOrDefault(x => x.Id == clinicId);

            this.clinicsRepository.Delete(clinic);
            await this.clinicsRepository.SaveChangesAsync();
        }

        public int GetCount()
        {
            return this.clinicsRepository.All().Count();
        }
    }
}
