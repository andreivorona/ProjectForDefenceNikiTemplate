namespace PetsDate.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PetsDate.Data.Models;
    using PetsDate.Services.Data;
    using PetsDate.Web.ViewModels.SosSignal;

    public class SosSignalController : Controller
    {
        private readonly ISosSignalService sosSignalService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<ApplicationUser> userManager;

        public SosSignalController(
            ISosSignalService sosSignalService,
            IWebHostEnvironment webHostEnvironment,
            UserManager<ApplicationUser> userManager)
        {
            this.sosSignalService = sosSignalService;
            this.webHostEnvironment = webHostEnvironment;
            this.userManager = userManager;
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

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.sosSignalService.CreateAsync(input, user.Id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            // todo return to Clinic info
            return this.Redirect("/SosSignal/All");
        }

        [Authorize]
        public async Task<IActionResult> Info(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = this.sosSignalService.GetInfo(user.Id, id);

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

            var viewModel = new SosSignalListViewModel
            {
                SosSignals = this.sosSignalService.GetAll(id, user.Id, itemPerPage),
                ItemPerPage = itemPerPage,
                PageNumber = id,
                ItemsCount = this.sosSignalService.GetCount(),
            };

            return this.View(viewModel);
        }
    }
}
