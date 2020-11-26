namespace PetsDate.Services.Data
{
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

        public async Task CreateAsync(CreateClinicInputModel input, string userId)
        {
            var clinic = new Clinic
            {
                UserId = userId,
                Name = input.Name,
                Location = input.Location,
            };

            await this.clinicsRepository.AddAsync(clinic);
            await this.clinicsRepository.SaveChangesAsync();
        }
    }
}
