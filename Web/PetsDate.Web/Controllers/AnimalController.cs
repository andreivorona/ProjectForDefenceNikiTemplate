namespace PetsDate.Web.Controllers
{
    using System;
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
            try
            {
                await this.animalService.CreateAsync(input, user.Id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            // todo redirect to animal info page
            return this.Redirect("/Animal/All");
        }

        // Animal/All/8
        [Authorize]
        public async Task<IActionResult> All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int itemPerPage = 6;

            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = new AnimalListViewModel
            {
                ItemPerPage = itemPerPage,
                PageNumber = id,
                Animals = this.animalService.GetAll(id, user.Id, itemPerPage),
                ItemsCount = this.animalService.GetCount(),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult AddImages(int id)
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddImages(AddImagesInputModel input, int id)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.animalService.CreateAnimalImageAsync(input, user.Id, id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            return this.Redirect("/Animal/All");
        }

        [Authorize]
        public async Task<IActionResult> ImagesCollection(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = new AnimalListViewModel
            {
                AnimalImages = this.animalService.GetAnimalImages(user.Id, id),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public async Task<IActionResult> Info(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = this.animalService.GetInfo(user.Id, id);

            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            this.animalService.Remove(id);

            return this.Redirect("/Animal/All");
        }
    }
}
