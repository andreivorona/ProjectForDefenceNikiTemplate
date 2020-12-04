namespace PetsDate.Web.ViewModels.Event
{
    using System.Collections.Generic;

    public class EventListViewModel : PagingViewModel
    {
        public IEnumerable<EventListAllViewModel> Events { get; set; }
    }
}
