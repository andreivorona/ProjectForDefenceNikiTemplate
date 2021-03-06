﻿namespace PetsDate.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using PetsDate.Web.ViewModels.Hotel;

    public interface IHotelService
    {
        Task CreateAsync(CreateHotelInputModel input, string userId);

        IEnumerable<HotelListAllViewModel> GetAll(int page, string userId, int itemsPerPage);

        IEnumerable<HotelListAllViewModel> GetAllHomePage();

        HotelListAllViewModel GetInfo(string userId, string hotelId);

        EditHotelInputModel GetById(string hotelId);

        Task UpdateAsync(string hotelId, EditHotelInputModel input);

        Task DeleteAsync(string hotelId);

        int GetCount();
    }
}
