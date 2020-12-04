namespace PetsDate.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PetsDate.Data.Models;
    using PetsDate.Services.Data;
    using PetsDate.Web.ViewModels.Publication;

    public class PublicationController : Controller
    {
        private readonly IPublicationService publicationService;
        private readonly UserManager<ApplicationUser> userManager;

        public PublicationController(
            IPublicationService publicationService,
            UserManager<ApplicationUser> userManager)
        {
            this.publicationService = publicationService;
            this.userManager = userManager;
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

            var user = await this.userManager.GetUserAsync(this.User);

            await this.publicationService.CreateAsync(input, user.Id);

            // todo return to Clinic info
            return this.Redirect("/");
        }

        [Authorize]
        public IActionResult All(int id = 1)
        {
            const int itemPerPage = 6;

            var viewModel = new PublicationListViewModel
            {
                Publications = this.publicationService.GetAll(),
                ItemPerPage = itemPerPage,
                PageNumber = id,
                ItemsCount = this.publicationService.GetCount(),
            };

            return this.View(viewModel);
        }
    }
}
