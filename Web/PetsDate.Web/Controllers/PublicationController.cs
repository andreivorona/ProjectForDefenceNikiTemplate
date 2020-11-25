namespace PetsDate.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PetsDate.Services.Data;
    using PetsDate.Web.ViewModels.Publication;

    public class PublicationController : Controller
    {
        private readonly IPublicationService publicationService;

        public PublicationController(IPublicationService publicationService)
        {
            this.publicationService = publicationService;
        }

        [Authorize]
        public IActionResult Create()
        {
            return this.View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreatePublicationInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.publicationService.CreateAsync(input);

            // todo return to Clinic info
            return this.Redirect("/");
        }
    }
}
