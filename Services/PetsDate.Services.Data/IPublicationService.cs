namespace PetsDate.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PetsDate.Web.ViewModels.Publication;

    public interface IPublicationService
    {
        Task CreateAsync(CreatePublicationInputModel input, string userId);

        IEnumerable<PublicationListAllViewModel> GetAll(int page, string userId, int itemsPerPage);

        IEnumerable<PublicationListAllViewModel> GetAllHomePage();

        PublicationListAllViewModel GetInfo(string userId, string publicationId);

        EditPublicationInputModel GetById(string publicationId);

        Task UpdateAsync(string publicationId, EditPublicationInputModel input);

        int GetCount();
    }
}
