namespace PetsDate.Web.ViewModels.Animal
{
    using System.ComponentModel.DataAnnotations;

    public class CreateAnimalInputModel
    {
        [MinLength(2)]
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, 400)]
        public int Age { get; set; }

        [Required]
        [MinLength(2)]
        public string Color { get; set; }

        [Required]
        [Range(1, 10000)]
        public double Weight { get; set; }

        public int CategoryId { get; set; }
    }
}
