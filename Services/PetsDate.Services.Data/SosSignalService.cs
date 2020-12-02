namespace PetsDate.Services.Data
{
    using System;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using PetsDate.Data.Common.Repositories;
    using PetsDate.Data.Models;
    using PetsDate.Web.ViewModels.SosSignal;

    public class SosSignalService : ISosSignalService
    {
        private readonly IDeletableEntityRepository<SosSignal> sosSignalsRepository;

        public SosSignalService(
            IDeletableEntityRepository<SosSignal> sosSignalsRepository)
        {
            this.sosSignalsRepository = sosSignalsRepository;
        }

        public async Task CreateAsync(CreateSosSignalInputModel input, string userId, string imageUrl)
        {
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
    }
}
