namespace PetsDate.Web.Controllers
{
    using System.Threading.Tasks;

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

        [HttpPost]
        public async Task<IActionResult> Create(CreateClinicInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.clinicService.CreateAsync(input, user.Id);

            // todo return to Clinic info
            return this.Redirect("/");
        }
    }
}
