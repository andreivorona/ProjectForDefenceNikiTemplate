namespace PetsDate.Web.Controllers
{
    using System;
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

            try
            {
                await this.hotelService.CreateAsync(input, user.Id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            // todo return to Hotel info
            return this.Redirect("/Hotel/All");
        }

        [Authorize]
        public async Task<IActionResult> All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            const int itemPerPage = 6;

            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = new HotelListViewModel
            {
                Hotels = this.hotelService.GetAll(id, user.Id, itemPerPage),
                ItemPerPage = itemPerPage,
                PageNumber = id,
                ItemsCount = this.hotelService.GetCount(),
            };

            return this.View(viewModel);
        }
    }
}
