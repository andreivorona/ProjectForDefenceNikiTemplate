namespace PetsDate.Web.Controllers
{
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
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
        private readonly IRepository<SosImage> imagesRepository;
        private readonly Cloudinary cloudinary;
        private readonly ICloudinaryService cloudinaryService;

        public SosSignalController(
            ISosSignalService sosSignalService,
            IWebHostEnvironment webHostEnvironment,
            UserManager<ApplicationUser> userManager,
            IRepository<SosImage> imagesRepository,
            Cloudinary cloudinary,
            ICloudinaryService cloudinaryService)
        {
            this.sosSignalService = sosSignalService;
            this.webHostEnvironment = webHostEnvironment;
            this.userManager = userManager;
            this.imagesRepository = imagesRepository;
            this.cloudinary = cloudinary;
            this.cloudinaryService = cloudinaryService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSosSignalInputModel input)
        {
            if (input.Image.Length > 10 * 1024 * 1024)
            {
                this.ModelState.AddModelError("Image", "File too big.");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.sosSignalService.CreateAsync(input, user.Id);

            if (input.Image.FileName.EndsWith(".png") || input.Image.FileName.EndsWith(".jpg"))
            {
                var imageUrl = await this.cloudinaryService.UploadAsync(this.cloudinary, input.Image);

                var sosImage = new SosImage
                {
                    Extension = imageUrl,
                };

                await this.imagesRepository.AddAsync(sosImage);
                await this.imagesRepository.SaveChangesAsync();
            }
            else
            {
                this.ModelState.AddModelError("Image", "Invalid File Type.");
            }

            // todo return to Clinic info
            return this.Redirect("/");
        }
    }
}
