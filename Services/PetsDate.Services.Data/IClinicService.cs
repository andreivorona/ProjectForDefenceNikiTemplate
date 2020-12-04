﻿namespace PetsDate.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PetsDate.Web.ViewModels.Clinic;

    public interface IClinicService
    {
        Task CreateAsync(CreateClinicInputModel input, string userId, string imageUrl);

        IEnumerable<ClinicListAllViewModel> GetAll();

        int GetCount();
    }
}
