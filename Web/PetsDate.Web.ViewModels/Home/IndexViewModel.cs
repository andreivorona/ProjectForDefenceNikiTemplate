﻿namespace PetsDate.Web.ViewModels.Home
{
    using System.Collections.Generic;

    using PetsDate.Data.Models;
    using PetsDate.Web.ViewModels.Animal;
    using PetsDate.Web.ViewModels.Clinic;
    using PetsDate.Web.ViewModels.Hotel;
    using PetsDate.Web.ViewModels.Publication;
    using PetsDate.Web.ViewModels.SosSignal;

    public class IndexViewModel
    {
        public int UsersCount { get; set; }

        public int AnimalsCount { get; set; }

        public int ClinicCount { get; set; }

        public int PublicationCount { get; set; }

        public int HoteslCount { get; set; }

        public int SosSignslsCount { get; set; }

        public int CategoriesCount { get; set; }

        public IEnumerable<AnimalListAllViewModel> Animals { get; set; }

        public IEnumerable<ClinicListAllViewModel> Clinics { get; set; }

        public IEnumerable<PublicationListAllViewModel> Publications { get; set; }

        public IEnumerable<HotelListAllViewModel> Hotels { get; set; }

        public IEnumerable<SosSignalListAllViewModel> SosSignals { get; set; }

        public ICollection<ApplicationUser> Users { get; set; } = new HashSet<ApplicationUser>();
    }
}
