namespace PetsDate.Web.Controllers
{
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PetsDate.Data.Models;
    using PetsDate.Services.Data;
    using PetsDate.Web.ViewModels.Hotel;

    public class HotelController : Controller
    {
        private readonly IHotelService hotelService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly Cloudinary cloudinary;
        private readonly ICloudinaryService cloudinaryService;

        public HotelController(
            IHotelService hotelService,
            UserManager<ApplicationUser> userManager,
            Cloudinary cloudinary,
            ICloudinaryService cloudinaryService)
        {
            this.hotelService = hotelService;
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
        public async Task<IActionResult> Create(CreateHotelInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var imageUrl = await this.cloudinaryService.UploadAsync(this.cloudinary, input.Image);

            var user = await this.userManager.GetUserAsync(this.User);

            await this.hotelService.CreateAsync(input, user.Id, imageUrl);

            // todo return to Hotel info
            return this.Redirect("/Hotel/All");
        }

        [Authorize]
        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int itemPerPage = 6;

            var viewModel = new HotelListViewModel
            {
                Hotels = this.hotelService.GetAll(),
                ItemPerPage = itemPerPage,
                PageNumber = id,
                ItemsCount = this.hotelService.GetCount(),
            };

            return this.View(viewModel);
        }
    }
}
