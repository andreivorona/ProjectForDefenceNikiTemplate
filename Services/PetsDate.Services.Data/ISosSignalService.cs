namespace PetsDate.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PetsDate.Web.ViewModels.SosSignal;

    public interface ISosSignalService
    {
        Task CreateAsync(CreateSosSignalInputModel input, string userId);

        IEnumerable<SosSignalListAllViewModel> GetAll(int page, string userId, int itemsPerPage);

        IEnumerable<SosSignalListAllViewModel> GetAllHomePage();

        SosSignalListAllViewModel GetInfo(string userId, string sosSignalId);

        EditSosSignalInputModel GetById(string sosSignalId);

        Task UpdateAsync(string sosSignalId, EditSosSignalInputModel input);

        Task DeleteAsync(string sosSignalId);

        int GetCount();
    }
}
