namespace PetsDate.Web.ViewModels.Clinic
{
    using System.ComponentModel.DataAnnotations;

    public class EditClinicInputModel
    {
        public string Id { get; set; }

        [Required]
        [MinLength(1)]
        [Display(Name = "Name as text")]
        public string Name { get; set; }

        [Required]
        [MinLength(1)]
        [Display(Name = "Location as text")]
        public string Location { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}
