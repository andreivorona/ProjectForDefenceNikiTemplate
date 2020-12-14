namespace PetsDate.Web.ViewModels.Votes
{
    using System.ComponentModel.DataAnnotations;

    public class PostVoteInputModel
    {
        public int AnimalId { get; set; }

        [Range(1, 5)]
        public int Value { get; set; }
    }
}
