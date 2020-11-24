namespace PetsDate.Web.ViewModels.SosSignal
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class CreateSosSignalInputModel
    {
        [Required]
        [Display(Name = "Name as text")]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Description as text")]
        [MinLength(2)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Location as text")]
        [MinLength(2)]
        public string Location { get; set; }

        [Display(Name = "Only Png and Jpg Images")]
        public IFormFile Image { get; set; }
    }
}
