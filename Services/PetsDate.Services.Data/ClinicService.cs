namespace PetsDate.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PetsDate.Data.Common.Repositories;
    using PetsDate.Data.Models;
    using PetsDate.Web.ViewModels.Clinic;

    public class ClinicService : IClinicService
    {
        private readonly IDeletableEntityRepository<Clinic> clinicsRepository;

        public ClinicService(IDeletableEntityRepository<Clinic> clinicsRepository)
        {
            this.clinicsRepository = clinicsRepository;
        }

        public async Task CreateAsync(CreateClinicInputModel input, string userId, string imageUrl)
        {
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

        public IEnumerable<ClinicListAllViewModel> GetAll()
        {
            return this.clinicsRepository.AllAsNoTracking()
                .Select(x => new ClinicListAllViewModel
                {
                    Name = x.Name,
                    Location = x.Location,
                    ImageUrl = x.ImageUrl,
                }).ToList();
        }
    }
}
