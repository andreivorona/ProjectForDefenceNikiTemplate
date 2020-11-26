namespace PetsDate.Services.Data
{
    using System.Threading.Tasks;

    using PetsDate.Web.ViewModels.Animal;

    public interface IAnimalService
    {
        Task CreateAsync(CreateAnimalInputModel input, string userId);
    }
}
