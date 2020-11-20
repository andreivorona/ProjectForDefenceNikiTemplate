namespace PetsDate.Web.ViewModels.Publication
{
    using System.ComponentModel.DataAnnotations;

    public class CreatePublicationInputModel
    {
        [Required]
        [MinLength(1)]
        [Display(Name = "Descripnion as text")]
        public string Description { get; set; }
    }
}
