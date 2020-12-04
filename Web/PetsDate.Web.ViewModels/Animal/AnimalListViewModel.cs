namespace PetsDate.Web.ViewModels.Animal
{
    using System;
    using System.Collections.Generic;

    public class AnimalListViewModel
    {
        public IEnumerable<AnimalListAllViewModel> Animals { get; set; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int PreviousPageNumber => this.PageNumber - 1;

        public int NextPageNumber => this.PageNumber + 1;

        public int PagesCount => (int)Math.Ceiling((double)this.AnimalsCount / this.ItemPerPage);

        public int PageNumber { get; set; }

        public int AnimalsCount { get; set; }

        public int ItemPerPage { get; set; }
    }
}
