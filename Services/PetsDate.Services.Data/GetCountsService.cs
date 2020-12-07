namespace PetsDate.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using PetsDate.Data.Common.Repositories;
    using PetsDate.Data.Models;
    using PetsDate.Web.ViewModels.Home;

    public class GetCountsService : IGetCountsService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly IDeletableEntityRepository<Animal> animalsRepository;
        private readonly IDeletableEntityRepository<Clinic> clinicsRepository;
        private readonly IDeletableEntityRepository<Publication> publicationRepository;
        private readonly IDeletableEntityRepository<Event> eventsRepository;
        private readonly IDeletableEntityRepository<Hotel> hotelsRepository;
        private readonly IDeletableEntityRepository<SosSignal> sosSignslsRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public GetCountsService(
            IDeletableEntityRepository<Category> categoriesRepository,
            IDeletableEntityRepository<Animal> animalsRepository,
            IDeletableEntityRepository<Clinic> clinicsRepository,
            IDeletableEntityRepository<Publication> publicationRepository,
            IDeletableEntityRepository<Event> eventsRepository,
            IDeletableEntityRepository<Hotel> hotelsRepository,
            IDeletableEntityRepository<SosSignal> sosSignslsRepository,
            IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.categoriesRepository = categoriesRepository;
            this.animalsRepository = animalsRepository;
            this.clinicsRepository = clinicsRepository;
            this.publicationRepository = publicationRepository;
            this.eventsRepository = eventsRepository;
            this.hotelsRepository = hotelsRepository;
            this.sosSignslsRepository = sosSignslsRepository;
            this.userRepository = userRepository;
        }

        public IndexViewModel GetCounts()
        {
            var data = new IndexViewModel
            {
                UsersCount = this.userRepository.All().Count(),
                AnimalsCount = this.animalsRepository.All().Count(),
                ClinicCount = this.clinicsRepository.All().Count(),
                EventsCount = this.eventsRepository.All().Count(),
                HoteslCount = this.hotelsRepository.All().Count(),
                PublicationCount = this.publicationRepository.All().Count(),
                SosSignslsCount = this.sosSignslsRepository.All().Count(),
                CategoriesCount = this.categoriesRepository.All().Count(),
            };

            return data;
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return this.userRepository.AllAsNoTracking()
                .Where(x => x.Roles.Count == 0)
                .Select(x => new ApplicationUser
                {
                     UserName = x.UserName,
                     Email = x.Email,
                     PhoneNumber = x.PhoneNumber,
                }).ToList();
        }
    }
}
