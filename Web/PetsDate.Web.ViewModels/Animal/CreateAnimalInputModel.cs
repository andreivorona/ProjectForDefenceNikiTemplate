namespace PetsDate.Web.ViewModels.Animal
{
    using System.ComponentModel.DataAnnotations;

    public class CreateAnimalInputModel
    {
        [MinLength(2)]
        [Required]
        [Display(Name = "Name as text")]
        public string Name { get; set; }

        [Required]
        [Range(0, 400)]
        [Display(Name = "Age is integer")]
        public int Age { get; set; }

        [Required]
        [MinLength(2)]
        [Display(Name = "Color as text")]
        public string Color { get; set; }

        [Required]
        [Range(0, 10000)]
        [Display(Name = "Weight in kilograms")]
        public double Weight { get; set; }

        public int CategoryId { get; set; }
    }
}
