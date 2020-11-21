namespace PetsDate.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using PetsDate.Services.Data;
    using PetsDate.Web.ViewModels.SosSignal;

    public class SosSignalController : Controller
    {
        private readonly ISosSignalService sosSignalService;

        public SosSignalController(ISosSignalService sosSignalService)
        {
            this.sosSignalService = sosSignalService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSosSignalInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.sosSignalService.CreateAsync(input);

            // todo return to Clinic info
            return this.Redirect("/");
        }
    }
}
