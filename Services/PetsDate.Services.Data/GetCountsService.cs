﻿namespace PetsDate.Services.Data
{
    using System.Linq;

    using PetsDate.Data.Common.Repositories;
    using PetsDate.Data.Models;
    using PetsDate.Web.ViewModels.Home;

    public class GetCountsService : IGetCountsService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly IDeletableEntityRepository<Animal> animalsRepository;
        private readonly IRepository<Image> imagesRepository;
        private readonly IDeletableEntityRepository<Clinic> clinicsRepository;
        private readonly IRepository<ClinicImage> clinicImagesRepository;
        private readonly IDeletableEntityRepository<Publication> publicationRepository;
        private readonly IDeletableEntityRepository<Event> eventsRepository;
        private readonly IDeletableEntityRepository<Hotel> hotelsRepository;
        private readonly IRepository<HotelImage> hotelImagesRepository;
        private readonly IDeletableEntityRepository<SosSignal> sosSignslsRepository;
        private readonly IRepository<SosImage> sosImagesRepository;

        public GetCountsService(
            IDeletableEntityRepository<Category> categoriesRepository,
            IDeletableEntityRepository<Animal> animalsRepository,
            IRepository<Image> imagesRepository,
            IDeletableEntityRepository<Clinic> clinicsRepository,
            IRepository<ClinicImage> clinicImagesRepository,
            IDeletableEntityRepository<Publication> publicationRepository,
            IDeletableEntityRepository<Event> eventsRepository,
            IDeletableEntityRepository<Hotel> hotelsRepository,
            IRepository<HotelImage> hotelImagesRepository,
            IDeletableEntityRepository<SosSignal> sosSignslsRepository,
            IRepository<SosImage> sosImagesRepository)
        {
            this.categoriesRepository = categoriesRepository;
            this.animalsRepository = animalsRepository;
            this.imagesRepository = imagesRepository;
            this.clinicsRepository = clinicsRepository;
            this.clinicImagesRepository = clinicImagesRepository;
            this.publicationRepository = publicationRepository;
            this.eventsRepository = eventsRepository;
            this.hotelsRepository = hotelsRepository;
            this.hotelImagesRepository = hotelImagesRepository;
            this.sosSignslsRepository = sosSignslsRepository;
            this.sosImagesRepository = sosImagesRepository;
        }

        public IndexViewModel GetCounts()
        {
            var data = new IndexViewModel
            {
                AnimalsCount = this.animalsRepository.All().Count(),
                AnimalImagesCount = this.imagesRepository.All().Count(),
                ClinicCount = this.clinicsRepository.All().Count(),
                ClinicImagesCount = this.clinicImagesRepository.All().Count(),
                EventsCount = this.eventsRepository.All().Count(),
                HotelImagesCount = this.hotelImagesRepository.All().Count(),
                HoteslCount = this.hotelsRepository.All().Count(),
                PublicationCount = this.publicationRepository.All().Count(),
                SosSignslsCount = this.sosSignslsRepository.All().Count(),
                SosImagesCount = this.sosImagesRepository.All().Count(),
                CategoriesCount = this.categoriesRepository.All().Count(),
            };

            return data;
        }
    }
}