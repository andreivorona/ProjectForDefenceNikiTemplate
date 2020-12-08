namespace PetsDate.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PetsDate.Web.ViewModels.Animal;

    public interface IAnimalService
    {
        Task CreateAsync(CreateAnimalInputModel input, string userId, string imageUrl);

        Task CreateAnimalImageAsync(AddImagesInputModel input, string userId, int animalId);

        IEnumerable<AnimalListAllViewModel> GetAll(int page, string userId, int itemsPerPage = 12);

        IEnumerable<AnimalImageListAllViewModel> GetAnimalImages(string userId, int animalId);

        int GetCount();

        int GetImagesCount();
    }
}
