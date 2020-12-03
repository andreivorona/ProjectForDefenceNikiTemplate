namespace PetsDate.Web.ViewModels.Animal
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

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

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CategoriesItems { get; set; }
    }
}
