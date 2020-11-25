namespace PetsDate.Web.Controllers
{
    using System.IO;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using PetsDate.Services.Data;
    using PetsDate.Web.ViewModels.SosSignal;

    public class SosSignalController : Controller
    {
        private readonly ISosSignalService sosSignalService;
        private readonly IWebHostEnvironment webHostEnvironment;

        public SosSignalController(
            ISosSignalService sosSignalService,
            IWebHostEnvironment webHostEnvironment)
        {
            this.sosSignalService = sosSignalService;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSosSignalInputModel input)
        {
            if (input.Image.FileName.EndsWith(".png") || input.Image.FileName.EndsWith(".jpg"))
            {
                using (FileStream fs = new FileStream(this.webHostEnvironment.WebRootPath + $"/SosImages/{input.Image.FileName}", FileMode.Create))
                {
                    await input.Image.CopyToAsync(fs);
                }
            }
            else
            {
                this.ModelState.AddModelError("Image", "Invalid File Type.");
            }

            if (input.Image.Length > 10 * 1024 * 1024)
            {
                this.ModelState.AddModelError("Image", "File too big.");
            }

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
