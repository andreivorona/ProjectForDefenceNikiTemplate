namespace PetsDate.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PetsDate.Web.ViewModels.Animal;

    public interface IAnimalService
    {
        Task CreateAsync(CreateAnimalInputModel input, string userId);

        Task CreateAnimalImageAsync(AddImagesInputModel input, string userId, int animalId);

        IEnumerable<AnimalListAllViewModel> GetAll(int page, string userId, int itemsPerPage = 12);

        IEnumerable<AnimalImageListAllViewModel> GetAnimalImages(string userId, int animalId);

        AnimalListAllViewModel GetInfo(string userId, int animalId);

        IEnumerable<AnimalListAllViewModel> GetAllHomePage();

        AnimalListAllViewModel GetInfoHomePage(int animalId);

        IEnumerable<AnimalImageListAllViewModel> GetAnimalImagesHomePage(int animalId);

        int GetCount();

        int GetImagesCount();

        EditAnimalInputModel GetById(int animalId);

        Task UpdateAsync(int animalId, EditAnimalInputModel input);

        void Remove(int id);
    }
}
