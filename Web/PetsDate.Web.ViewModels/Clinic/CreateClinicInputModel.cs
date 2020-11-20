namespace PetsDate.Web.ViewModels.Clinic
{
    using System.ComponentModel.DataAnnotations;

    public class CreateClinicInputModel
    {
        [Required]
        [MinLength(1)]
        [Display(Name = "Name as text")]
        public string Name { get; set; }

        [Required]
        [MinLength(1)]
        [Display(Name = "Location as text")]
        public string Location { get; set; }
    }
}
