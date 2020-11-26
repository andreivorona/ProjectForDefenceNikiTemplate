namespace PetsDate.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PetsDate.Data.Models;
    using PetsDate.Services.Data;
    using PetsDate.Web.ViewModels.Animal;

    public class AnimalController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IAnimalService animalService;
        private readonly UserManager<ApplicationUser> userManager;

        public AnimalController(
            ICategoriesService categoriesService,
            IAnimalService animalService,
            UserManager<ApplicationUser> userManager)
        {
            this.categoriesService = categoriesService;
            this.animalService = animalService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateAnimalInputModel();
            viewModel.CategoriesItems = this.categoriesService.GetAllKeyValuePairs();

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateAnimalInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoriesItems = this.categoriesService.GetAllKeyValuePairs();

                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            ////var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value; info from cookie

            // create animal using service method
            await this.animalService.CreateAsync(input, user.Id);

            // todo redirect to animal info page
            return this.Redirect("/");
        }
    }
}
