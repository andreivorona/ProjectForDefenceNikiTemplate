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
        private readonly Cloudinary cloudinary;
        private readonly ICloudinaryService cloudinaryService;

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
            this.cloudinary = cloudinary;
            this.cloudinaryService = cloudinaryService;
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

            var imageUrl = await this.cloudinaryService.UploadAsync(this.cloudinary, input.Image);

            var user = await this.userManager.GetUserAsync(this.User);

            ////var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value; info from cookie

            // create animal using service method
            await this.animalService.CreateAsync(input, user.Id, imageUrl);

            // todo redirect to animal info page
            return this.Redirect("/Animal/All");
        }

        // Animal/All/8
        [Authorize]
        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int itemPerPage = 6;

            var viewModel = new AnimalListViewModel
            {
                ItemPerPage = itemPerPage,
                PageNumber = id,
                Animals = this.animalService.GetAll(id, itemPerPage),
                ItemsCount = this.animalService.GetCount(),
            };

            return this.View(viewModel);
        }
    }
}
