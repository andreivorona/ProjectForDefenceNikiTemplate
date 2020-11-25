namespace PetsDate.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using PetsDate.Services.Data;
    using PetsDate.Web.ViewModels.Event;

    public class EventController : Controller
    {
        private readonly IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }

        public IActionResult Create()
        {
            var model = new CreateEventInputModel
            {
                BeginEvent = DateTime.Parse(DateTime.UtcNow.ToString("f")),
                EndEvent = DateTime.Parse(DateTime.UtcNow.AddDays(1).ToString("f")),
            };

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEventInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.eventService.CreateAsync(input);

            // todo return to Clinic info
            return this.Redirect("/");
        }
    }
}
