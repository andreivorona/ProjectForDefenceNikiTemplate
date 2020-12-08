namespace PetsDate.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
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
            UserManager<ApplicationUser> userManager,
            Cloudinary cloudinary,
            ICloudinaryService cloudinaryService)
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

            await this.animalService.CreateAnimalImageAsync(input, user.Id, id);

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
    }
}
