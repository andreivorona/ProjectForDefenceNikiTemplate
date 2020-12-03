namespace PetsDate.Web.Controllers
{
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PetsDate.Data.Models;
    using PetsDate.Services.Data;
    using PetsDate.Web.ViewModels.Clinic;

    public class ClinicController : Controller
    {
        private readonly IClinicService clinicService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly Cloudinary cloudinary;
        private readonly ICloudinaryService cloudinaryService;

        public ClinicController(
            IClinicService clinicService,
            UserManager<ApplicationUser> userManager,
            Cloudinary cloudinary,
            ICloudinaryService cloudinaryService)
        {
            this.clinicService = clinicService;
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
        public async Task<IActionResult> Create(CreateClinicInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var imageUrl = await this.cloudinaryService.UploadAsync(this.cloudinary, input.Image);

            var user = await this.userManager.GetUserAsync(this.User);

            await this.clinicService.CreateAsync(input, user.Id, imageUrl);

            // todo return to Clinic info
            return this.Redirect("/Clinic/All");
        }

        [Authorize]
        public IActionResult All()
        {
            var viewModel = new ClinicListViewModel
            {
                Clinics = this.clinicService.GetAll(),
            };

            return this.View(viewModel);
        }
    }
}
