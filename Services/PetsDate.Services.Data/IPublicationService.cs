namespace PetsDate.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PetsDate.Web.ViewModels.Publication;

    public interface IPublicationService
    {
        Task CreateAsync(CreatePublicationInputModel input, string userId);

        IEnumerable<PublicationListAllViewModel> GetAll(int page, string userId, int itemsPerPage);

        int GetCount();
    }
}
