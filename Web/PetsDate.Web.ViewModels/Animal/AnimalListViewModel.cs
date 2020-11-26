namespace PetsDate.Web.ViewModels.Animal
{
    using System.Collections.Generic;

    public class AnimalListViewModel
    {
        public IEnumerable<AnimalListAllViewModel> Animals { get; set; }

        public int PageNumber { get; set; }
    }
}
