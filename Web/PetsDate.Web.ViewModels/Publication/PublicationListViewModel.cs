namespace PetsDate.Web.ViewModels.Publication
{
    using System.Collections.Generic;

    public class PublicationListViewModel
    {
        public IEnumerable<PublicationListAllViewModel> Publications { get; set; }
    }
}
