namespace PetsDate.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CloudinaryDotNet;
    using PetsDate.Data.Common.Repositories;
    using PetsDate.Data.Models;
    using PetsDate.Web.ViewModels.Clinic;

    public class ClinicService : IClinicService
    {
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
                    Name = x.Name,
                    Location = x.Location,
                    ImageUrl = x.ImageUrl,
                }).ToList();
        }

        public int GetCount()
        {
            return this.clinicsRepository.All().Count();
        }
    }
}
