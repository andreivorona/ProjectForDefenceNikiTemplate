namespace PetsDate.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PetsDate.Web.ViewModels.Hotel;

    public interface IHotelService
    {
        Task CreateAsync(CreateHotelInputModel input, string userId, string imageUrl);

        IEnumerable<HotelListAllViewModel> GetAll();

        int GetCount();
    }
}
