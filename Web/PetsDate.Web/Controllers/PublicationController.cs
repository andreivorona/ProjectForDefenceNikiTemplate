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

            return this.Redirect("/Publication/All");
        }

        [Authorize]
        public async Task<IActionResult> Info(string id)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var viewModel = this.publicationService.GetInfo(user.Id, id);

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

            var viewModel = new PublicationListViewModel
            {
                Publications = this.publicationService.GetAll(id, user.Id, itemPerPage),
                ItemPerPage = itemPerPage,
                PageNumber = id,
                ItemsCount = this.publicationService.GetCount(),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Edit(string id)
        {
            var viewModel = this.publicationService.GetById(id);

            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(string id, EditPublicationInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.publicationService.UpdateAsync(id, input);

            return this.Redirect("/Publication/All");
        }
    }
}
