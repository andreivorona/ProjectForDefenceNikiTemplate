namespace PetsDate.Web.ViewModels.Animal
{
    using System;
    using System.Collections.Generic;

    public class AnimalListViewModel : PagingViewModel
    {
        public IEnumerable<AnimalListAllViewModel> Animals { get; set; }

        public IEnumerable<AnimalImageListAllViewModel> AnimalImages { get; set; }
    }
}
