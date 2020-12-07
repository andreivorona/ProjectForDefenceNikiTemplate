namespace PetsDate.Web.ViewModels.Animal
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class AddImagesInputModel
    {
        [Required]
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
