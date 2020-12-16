namespace PetsDate.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PetsDate.Web.ViewModels.Hotel;

    public interface IHotelService
    {
        Task CreateAsync(CreateHotelInputModel input, string userId);

        IEnumerable<HotelListAllViewModel> GetAll(int page, string userId, int itemsPerPage);

        IEnumerable<HotelListAllViewModel> GetAllHomePage();

        int GetCount();
    }
}
