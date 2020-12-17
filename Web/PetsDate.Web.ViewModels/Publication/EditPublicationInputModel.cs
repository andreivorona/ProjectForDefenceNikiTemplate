namespace PetsDate.Web.ViewModels.Publication
{
    using System.ComponentModel.DataAnnotations;

    public class EditPublicationInputModel
    {
        public string Id { get; set; }

        [Required]
        [MinLength(1)]
        [Display(Name = "Descripnion as text")]
        public string Description { get; set; }
    }
}
