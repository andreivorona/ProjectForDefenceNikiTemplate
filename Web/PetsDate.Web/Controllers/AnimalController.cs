namespace PetsDate.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PetsDate.Services.Data;
    using PetsDate.Web.ViewModels.Animal;
    using System.Threading.Tasks;

    public class AnimalController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IAnimalService animalService;

        public AnimalController(
            ICategoriesService categoriesService,
            IAnimalService animalService)
        {
            this.categoriesService = categoriesService;
            this.animalService = animalService;
        }

        public IActionResult Create()
        {
            var viewModel = new CreateAnimalInputModel();
            viewModel.CategoriesItems = this.categoriesService.GetAllKeyValuePairs();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAnimalInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetAllKeyValuePairs();

                return this.View(input);
            }

            // create animal using service method
            await this.animalService.CreateAsync(input);

            // todo redirect to animal info page
            return this.Redirect("/");
        }
    }
}
