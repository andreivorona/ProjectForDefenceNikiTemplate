namespace PetsDate.Web.ViewModels.Hotel
{
    using System.Collections.Generic;

    public class HotelListViewModel : PagingViewModel
    {
        public IEnumerable<HotelListAllViewModel> Hotels { get; set; }
    }
}
