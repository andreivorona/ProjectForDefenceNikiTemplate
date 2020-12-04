namespace PetsDate.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PetsDate.Data.Common.Repositories;
    using PetsDate.Data.Models;
    using PetsDate.Web.ViewModels.Event;

    public class EventService : IEventService
    {
        private readonly IDeletableEntityRepository<Event> eventsRepository;

        public EventService(IDeletableEntityRepository<Event> eventsRepository)
        {
            this.eventsRepository = eventsRepository;
        }

        public async Task CreateAsync(CreateEventInputModel input, string userId)
        {
            var eventItem = new Event
            {
                UserId = userId,
                Name = input.Name,
                Location = input.Location,
                BeginEvent = input.BeginEvent,
                EndEvent = input.EndEvent,
            };

            await this.eventsRepository.AddAsync(eventItem);
            await this.eventsRepository.SaveChangesAsync();
        }

        public IEnumerable<EventListAllViewModel> GetAll()
        {
            return this.eventsRepository.AllAsNoTracking()
                .Select(x => new EventListAllViewModel
                {
                    Username = x.User.UserName,
                    Name = x.Name,
                    Location = x.Location,
                    BeginEvent = x.BeginEvent,
                    EndEvent = x.EndEvent,
                }).ToList();
        }

        public int GetCount()
        {
            return this.eventsRepository.All().Count();
        }
    }
}
