namespace PetsDate.Web.ViewModels.Clinic
{
    using System.Collections.Generic;

    public class ClinicListViewModel
    {
        public IEnumerable<ClinicListAllViewModel> Clinics { get; set; }
    }
}
