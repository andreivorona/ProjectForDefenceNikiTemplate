namespace PetsDate.Web.Controllers
{
    using System;
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

        public ClinicController(
            IClinicService clinicService,
            UserManager<ApplicationUser> userManager)
        {
            this.clinicService = clinicService;
            this.userManager = userManager;
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

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.clinicService.CreateAsync(input, user.Id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            // todo return to Clinic info
            return this.Redirect("/Clinic/All");
        }

        [Authorize]
        public async Task<IActionResult> Info(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = this.clinicService.GetInfo(user.Id, id);

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

            var viewModel = new ClinicListViewModel
            {
                Clinics = this.clinicService.GetAll(id, user.Id, itemPerPage),
                ItemPerPage = itemPerPage,
                PageNumber = id,
                ItemsCount = this.clinicService.GetCount(),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Edit(string id)
        {
            var viewModel = this.clinicService.GetById(id);

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(string id, EditClinicInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.clinicService.UpdateAsync(id, input);

            return this.Redirect("/Clinic/All");
        }
    }
}
