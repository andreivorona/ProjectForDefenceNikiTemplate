namespace PetsDate.Web.ViewModels.SosSignal
{
    using System.ComponentModel.DataAnnotations;

    public class EditSosSignalInputModel
    {
        public string Id { get; set; }

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

        [Required]
        public string ImageUrl { get; set; }
    }
}
