namespace PetsDate.Web.ViewModels.Clinic
{
    using System.Collections.Generic;

    public class ClinicListViewModel : PagingViewModel
    {
        public IEnumerable<ClinicListAllViewModel> Clinics { get; set; }
    }
}
