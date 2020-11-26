namespace PetsDate.Services.Data
{
    using System.Threading.Tasks;

    using PetsDate.Data.Common.Repositories;
    using PetsDate.Data.Models;
    using PetsDate.Web.ViewModels.Hotel;

    public class HotelService : IHotelService
    {
        private readonly IDeletableEntityRepository<Hotel> hotelsRepository;

        public HotelService(IDeletableEntityRepository<Hotel> hotelsRepository)
        {
            this.hotelsRepository = hotelsRepository;
        }

        public async Task CreateAsync(CreateHotelInputModel input, string userId)
        {
            var clinic = new Hotel
            {
                UserId = userId,
                Name = input.Name,
                Location = input.Location,
                Description = input.Description,
            };

            await this.hotelsRepository.AddAsync(clinic);
            await this.hotelsRepository.SaveChangesAsync();
        }
    }
}
