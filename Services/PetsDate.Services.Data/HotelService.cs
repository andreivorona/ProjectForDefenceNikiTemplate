namespace PetsDate.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
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

        public async Task CreateAsync(CreateHotelInputModel input, string userId, string imageUrl)
        {
            var clinic = new Hotel
            {
                UserId = userId,
                Name = input.Name,
                Location = input.Location,
                Description = input.Description,
                ImageUrl = imageUrl,
            };

            await this.hotelsRepository.AddAsync(clinic);
            await this.hotelsRepository.SaveChangesAsync();
        }

        public IEnumerable<HotelListAllViewModel> GetAll()
        {
            return this.hotelsRepository.AllAsNoTracking()
                .Select(x => new HotelListAllViewModel
                {
                    Name = x.Name,
                    Location = x.Location,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                }).ToList();
        }
    }
}
