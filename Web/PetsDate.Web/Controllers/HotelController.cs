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

            return this.Redirect("/Hotel/All");
        }

        [Authorize]
        public async Task<IActionResult> Info(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = this.hotelService.GetInfo(user.Id, id);

            return this.View(viewModel);
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

        [Authorize]
        public IActionResult Edit(string id)
        {
            var viewModel = this.hotelService.GetById(id);

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(string id, EditHotelInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.hotelService.UpdateAsync(id, input);

            return this.Redirect("/Hotel/All");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await this.hotelService.DeleteAsync(id);

            return this.Redirect("/Hotel/All");
        }
    }
}
