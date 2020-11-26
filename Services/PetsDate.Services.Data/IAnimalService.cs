namespace PetsDate.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PetsDate.Web.ViewModels.Animal;

    public interface IAnimalService
    {
        Task CreateAsync(CreateAnimalInputModel input, string userId);

        IEnumerable<AnimalListAllViewModel> GetAll(int page, int itemsPerPage = 12);
    }
}
