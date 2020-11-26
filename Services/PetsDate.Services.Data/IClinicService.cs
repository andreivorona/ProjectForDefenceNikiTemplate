namespace PetsDate.Services.Data
{
    using System.Threading.Tasks;

    using PetsDate.Web.ViewModels.Clinic;

    public interface IClinicService
    {
        Task CreateAsync(CreateClinicInputModel input, string userId);
    }
}
