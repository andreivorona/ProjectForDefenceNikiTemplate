namespace PetsDate.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PetsDate.Services.Data;
    using PetsDate.Web.ViewModels.Animal;

    public class AnimalController : Controller
    {
        private readonly ICategoriesService categoriesService;

        public AnimalController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult Create()
        {
            var viewModel = new CreateAnimalInputModel();
            viewModel.CategoriesItems = this.categoriesService.GetAllKeyValuePairs();

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateAnimalInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetAllKeyValuePairs();

                return this.View(input);
            }

            // create animal using service method
            // todo redirect to animal info page
            return this.Redirect("/");
        }
    }
}
