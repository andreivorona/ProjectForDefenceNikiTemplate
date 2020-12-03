namespace PetsDate.Web.ViewModels.Clinic
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

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

        [Required]
        public IFormFile Image { get; set; }
    }
}
