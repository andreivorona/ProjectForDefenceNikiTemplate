namespace PetsDate.Web.ViewModels
{
    using System;

    public class PagingViewModel
    {
        public bool HasPreviousPage => this.PageNumber > 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int PreviousPageNumber => this.PageNumber - 1;

        public int NextPageNumber => this.PageNumber + 1;

        public int PagesCount => (int)Math.Ceiling((double)this.ItemsCount / this.ItemPerPage);

        public int PageNumber { get; set; }

        public int ItemPerPage { get; set; }

        public int ItemsCount { get; set; }
    }
}
