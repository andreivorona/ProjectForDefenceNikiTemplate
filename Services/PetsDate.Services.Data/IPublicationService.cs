namespace PetsDate.Services.Data
{
    using System.Threading.Tasks;

    using PetsDate.Web.ViewModels.Publication;

    public interface IPublicationService
    {
        Task CreateAsync(CreatePublicationInputModel input, string userId);
    }
}
