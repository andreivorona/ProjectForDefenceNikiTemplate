namespace PetsDate.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PetsDate.Web.ViewModels.Event;

    public interface IEventService
    {
        Task CreateAsync(CreateEventInputModel input, string userId);

        IEnumerable<EventListAllViewModel> GetAll();

        int GetCount();
    }
}
