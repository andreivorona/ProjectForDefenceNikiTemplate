namespace PetsDate.Services.Data
{
    using System.Threading.Tasks;

    using PetsDate.Web.ViewModels.Hotel;

    public interface IHotelService
    {
        Task CreateAsync(CreateHotelInputModel input, string userId);
    }
}
