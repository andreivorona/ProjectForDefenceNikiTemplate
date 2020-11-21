namespace PetsDate.Web.ViewModels.Event
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CreateEventInputModel
    {
        [Required]
        [MinLength(2)]
        [Display(Name = "Name as text")]
        public string Name { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name = "Location as text")]
        public string Location { get; set; }

        public DateTime BeginEvent { get; set; }

        public DateTime EndEvent { get; set; }
    }
}
