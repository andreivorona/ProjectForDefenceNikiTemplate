using System.ComponentModel.DataAnnotations;

namespace PetsDate.Web.ViewModels.SosSignal
{
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
    }
}
