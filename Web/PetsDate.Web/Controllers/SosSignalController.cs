namespace PetsDate.Web.Controllers
{
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PetsDate.Data.Common.Repositories;
    using PetsDate.Data.Models;
    using PetsDate.Services.Data;
    using PetsDate.Web.ViewModels.SosSignal;

    public class SosSignalController : Controller
    {
        private readonly ISosSignalService sosSignalService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly Cloudinary cloudinary;
        private readonly ICloudinaryService cloudinaryService;

        public SosSignalController(
            ISosSignalService sosSignalService,
            IWebHostEnvironment webHostEnvironment,
            UserManager<ApplicationUser> userManager,
            Cloudinary cloudinary,
            ICloudinaryService cloudinaryService)
        {
            this.sosSignalService = sosSignalService;
            this.webHostEnvironment = webHostEnvironment;
            this.userManager = userManager;
            this.cloudinary = cloudinary;
            this.cloudinaryService = cloudinaryService;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateSosSignalInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var imageUrl = await this.cloudinaryService.UploadAsync(this.cloudinary, input.Image);

            var user = await this.userManager.GetUserAsync(this.User);

            await this.sosSignalService.CreateAsync(input, user.Id, imageUrl);

            // todo return to Clinic info
            return this.Redirect("/SosSignal/All");
        }

        [Authorize]
        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int itemPerPage = 6;

            var viewModel = new SosSignalListViewModel
            {
                SosSignals = this.sosSignalService.GetAll(),
                ItemPerPage = itemPerPage,
                PageNumber = id,
                ItemsCount = this.sosSignalService.GetCount(),
            };

            return this.View(viewModel);
        }
    }
}
