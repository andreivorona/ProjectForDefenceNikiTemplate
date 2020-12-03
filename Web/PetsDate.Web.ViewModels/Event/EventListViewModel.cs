namespace PetsDate.Web.ViewModels.Event
{
    using System.Collections.Generic;

    public class EventListViewModel
    {
        public IEnumerable<EventListAllViewModel> Events { get; set; }
    }
}
