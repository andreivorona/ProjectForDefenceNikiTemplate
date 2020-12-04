namespace PetsDate.Web.ViewModels.SosSignal
{
    using System.Collections.Generic;

    public class SosSignalListViewModel : PagingViewModel
    {
        public IEnumerable<SosSignalListAllViewModel> SosSignals { get; set; }
    }
}
