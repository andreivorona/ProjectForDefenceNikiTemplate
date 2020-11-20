namespace PetsDate.Services.Data
{
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

        public async Task CreateAsync(CreatePublicationInputModel input)
        {
            var publication = new Publication
            {
                 Description = input.Description,
            };

            await this.publicationRepository.AddAsync(publication);
            await this.publicationRepository.SaveChangesAsync();
        }
    }
}
