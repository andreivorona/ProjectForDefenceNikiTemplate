namespace PetsDate.Services.Data
{
    using System.Threading.Tasks;

    using PetsDate.Web.ViewModels.SosSignal;

    public interface ISosSignalService
    {
        Task CreateAsync(CreateSosSignalInputModel input, string userId);
    }
}
