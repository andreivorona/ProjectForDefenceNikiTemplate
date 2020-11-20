namespace PetsDate.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PetsDate.Services.Data;
    using PetsDate.Web.ViewModels.Clinic;
    using System.Threading.Tasks;

    public class ClinicController : Controller
    {
        private readonly IClinicService clinicService;

        public ClinicController(IClinicService clinicService)
        {
            this.clinicService = clinicService;
        }

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

            await this.clinicService.CreateAsync(input);

            // todo return to Clinic info
            return this.Redirect("/");
        }
    }
}
