namespace PetsDate.Web.ViewModels.Hotel
{
    using System.ComponentModel.DataAnnotations;

    public class CreateHotelInputModel
    {
        [Required]
        [Display(Name = "Name as text")]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Location as text")]
        [MinLength(2)]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Description as text")]
        [MinLength(2)]
        public string Description { get; set; }
    }
}
