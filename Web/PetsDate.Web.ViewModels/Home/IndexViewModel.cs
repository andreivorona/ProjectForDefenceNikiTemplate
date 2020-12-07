using PetsDate.Data.Models;
using System.Collections.Generic;

namespace PetsDate.Web.ViewModels.Home
{
    public class IndexViewModel
    {
        public int UsersCount { get; set; }

        public int AnimalsCount { get; set; }

        public int ClinicCount { get; set; }

        public int PublicationCount { get; set; }

        public int EventsCount { get; set; }

        public int HoteslCount { get; set; }

        public int SosSignslsCount { get; set; }

        public int CategoriesCount { get; set; }

        public ICollection<ApplicationUser> Users { get; set; } = new HashSet<ApplicationUser>();
    }
}
