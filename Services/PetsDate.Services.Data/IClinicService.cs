namespace PetsDate.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PetsDate.Web.ViewModels.Clinic;

    public interface IClinicService
    {
        Task CreateAsync(CreateClinicInputModel input, string userId);

        IEnumerable<ClinicListAllViewModel> GetAll(int page, string userId, int itemsPerPage);

        IEnumerable<ClinicListAllViewModel> GetAllHomePage();

        ClinicListAllViewModel GetInfo(string userId, string clinicId);

        EditClinicInputModel GetById(string clinicId);

        Task UpdateAsync(string clinicId, EditClinicInputModel input);

        Task DeleteAsync(string clinicId);

        int GetCount();
    }
}
