﻿namespace PetsDate.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PetsDate.Data.Common.Repositories;
    using PetsDate.Data.Models;
    using PetsDate.Web.ViewModels.Publication;

    public class PublicationService : IPublicationService
    {
        private readonly IDeletableEntityRepository<Publication> publicationRepository;

        public PublicationService(IDeletableEntityRepository<Publication> publicationRepository)
        {
            this.publicationRepository = publicationRepository;
        }

        public async Task CreateAsync(CreatePublicationInputModel input, string userId)
        {
            var publication = new Publication
            {
                UserId = userId,
                Description = input.Description,
            };

            await this.publicationRepository.AddAsync(publication);
            await this.publicationRepository.SaveChangesAsync();
        }

        public IEnumerable<PublicationListAllViewModel> GetAll(int page, string userId, int itemsPerPage)
        {
            return this.publicationRepository.AllAsNoTracking()
                .Where(x => x.User.Id == userId)
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new PublicationListAllViewModel
                {
                    Id = x.Id,
                    UserName = x.User.UserName,
                    Description = x.Description,
                }).ToList();
        }

        public IEnumerable<PublicationListAllViewModel> GetAllHomePage()
        {
            return this.publicationRepository.AllAsNoTracking()
                .Select(x => new PublicationListAllViewModel
                {
                    Description = x.Description,
                    UserName = x.User.UserName,
                }).ToList();
        }

        public PublicationListAllViewModel GetInfo(string userId, string publicationId)
        {
            return this.publicationRepository.AllAsNoTracking()
                .Where(x => x.Id == publicationId && x.UserId == userId)
                .Select(x => new PublicationListAllViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    UserName = x.User.UserName,
                }).FirstOrDefault();
        }

        public EditPublicationInputModel GetById(string publicationId)
        {
            var result = this.publicationRepository.AllAsNoTracking()
                .Where(x => x.Id == publicationId)
                .Select(x => new EditPublicationInputModel
                {
                    Id = x.Id,
                    Description = x.Description,
                }).FirstOrDefault();

            return result;
        }

        public async Task UpdateAsync(string publicationId, EditPublicationInputModel input)
        {
            var hotel = this.publicationRepository.All()
                .FirstOrDefault(x => x.Id == publicationId);

            hotel.Description = input.Description;

            await this.publicationRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string publicationId)
        {
            var publication = this.publicationRepository.All()
                .FirstOrDefault(x => x.Id == publicationId);

            this.publicationRepository.Delete(publication);
            await this.publicationRepository.SaveChangesAsync();
        }

        public int GetCount()
        {
            return this.publicationRepository.All().Count();
        }
    }
}
