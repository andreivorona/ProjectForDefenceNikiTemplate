namespace PetsDate.Services.Data
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
                    Username = x.User.UserName,
                    Description = x.Description,
                }).ToList();
        }

        public IEnumerable<PublicationListAllViewModel> GetAllHomePage()
        {
            return this.publicationRepository.AllAsNoTracking()
                .Select(x => new PublicationListAllViewModel
                {
                    Username = x.User.UserName,
                    Description = x.Description,
                    UserName = x.User.UserName,
                }).ToList();
        }

        public int GetCount()
        {
            return this.publicationRepository.All().Count();
        }
    }
}
