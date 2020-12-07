namespace PetsDate.Services.Data
{
    using System.Collections.Generic;

    using PetsDate.Data.Models;
    using PetsDate.Web.ViewModels.Home;

    public interface IGetCountsService
    {
        // Use View model
        IndexViewModel GetCounts();

        public IEnumerable<ApplicationUser> GetAll();
    }
}
