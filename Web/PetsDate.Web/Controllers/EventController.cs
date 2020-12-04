namespace PetsDate.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using PetsDate.Data.Models;
    using PetsDate.Services.Data;
    using PetsDate.Web.ViewModels.Event;

    public class EventController : Controller
    {
        private readonly IEventService eventService;
        private readonly UserManager<ApplicationUser> userManager;

        public EventController(
            IEventService eventService,
            UserManager<ApplicationUser> userManager)
        {
            this.eventService = eventService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Create()
        {
            var model = new CreateEventInputModel
            {
                BeginEvent = DateTime.Parse(DateTime.UtcNow.ToString("f")),
                EndEvent = DateTime.Parse(DateTime.UtcNow.AddDays(1).ToString("f")),
            };

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateEventInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            await this.eventService.CreateAsync(input, user.Id);

            // todo return to Clinic info
            return this.Redirect("/");
        }

        [Authorize]
        public IActionResult All(int id = 1)
        {
            const int itemPerPage = 6;

            var viewModel = new EventListViewModel
            {
                Events = this.eventService.GetAll(),
                ItemPerPage = itemPerPage,
                PageNumber = id,
                ItemsCount = this.eventService.GetCount(),
            };

            return this.View(viewModel);
        }
    }
}
