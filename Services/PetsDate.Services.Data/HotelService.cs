﻿namespace PetsDate.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using PetsDate.Data.Common.Repositories;
    using PetsDate.Data.Models;
    using PetsDate.Web.ViewModels.Hotel;

    public class HotelService : IHotelService
    {
        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IDeletableEntityRepository<Hotel> hotelsRepository;
        private readonly Cloudinary cloudinary;
        private readonly ICloudinaryService cloudinaryService;

        public HotelService(
            IDeletableEntityRepository<Hotel> hotelsRepository,
            Cloudinary cloudinary,
            ICloudinaryService cloudinaryService)
        {
            this.hotelsRepository = hotelsRepository;
            this.cloudinary = cloudinary;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task CreateAsync(CreateHotelInputModel input, string userId)
        {
            var extension = Path.GetExtension(input.Image.FileName);

            if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            {
                throw new Exception($"Invalid image extension {extension}");
            }

            var imageUrl = await this.cloudinaryService.UploadAsync(this.cloudinary, input.Image);

            var clinic = new Hotel
            {
                UserId = userId,
                Name = input.Name,
                Location = input.Location,
                Description = input.Description,
                ImageUrl = imageUrl,
            };

            await this.hotelsRepository.AddAsync(clinic);
            await this.hotelsRepository.SaveChangesAsync();
        }

        public IEnumerable<HotelListAllViewModel> GetAll(int page, string userId, int itemsPerPage)
        {
            return this.hotelsRepository.AllAsNoTracking()
                .Where(x => x.User.Id == userId)
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new HotelListAllViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Location = x.Location,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                }).ToList();
        }

        public IEnumerable<HotelListAllViewModel> GetAllHomePage()
        {
            return this.hotelsRepository.AllAsNoTracking()
                .Select(x => new HotelListAllViewModel
                {
                    Name = x.Name,
                    Location = x.Location,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    UserName = x.User.UserName,
                }).ToList();
        }

        public HotelListAllViewModel GetInfo(string userId, string hotelId)
        {
            return this.hotelsRepository.AllAsNoTracking()
                .Where(x => x.Id == hotelId && x.UserId == userId)
                .Select(x => new HotelListAllViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Location = x.Location,
                    ImageUrl = x.ImageUrl,
                    UserName = x.User.UserName,
                }).FirstOrDefault();
        }

        public EditHotelInputModel GetById(string hotelId)
        {
            var result = this.hotelsRepository.AllAsNoTracking()
                .Where(x => x.Id == hotelId)
                .Select(x => new EditHotelInputModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Location = x.Location,
                    ImageUrl = x.ImageUrl,
                    Description = x.Description,
                }).FirstOrDefault();

            return result;
        }

        public async Task UpdateAsync(string hotelId, EditHotelInputModel input)
        {
            var hotel = this.hotelsRepository.All()
                .FirstOrDefault(x => x.Id == hotelId);

            hotel.Name = input.Name;
            hotel.ImageUrl = input.ImageUrl;
            hotel.Location = input.Location;
            hotel.Description = input.Description;

            await this.hotelsRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string hotelId)
        {
            var hotel = this.hotelsRepository.All()
                .FirstOrDefault(x => x.Id == hotelId);

            this.hotelsRepository.Delete(hotel);
            await this.hotelsRepository.SaveChangesAsync();
        }

        public int GetCount()
        {
            return this.hotelsRepository.All().Count();
        }
    }
}
