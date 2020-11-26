namespace PetsDate.Web.Controllers
{
    using System.Threading.Tasks;

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

        public HotelController(
            IHotelService hotelService,
            UserManager<ApplicationUser> userManager)
        {
            this.hotelService = hotelService;
            this.userManager = userManager;
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

            var user = await this.userManager.GetUserAsync(this.User);

            await this.hotelService.CreateAsync(input, user.Id);

            // todo return to Hotel info
            return this.Redirect("/");
        }
    }
}
