namespace PetsDate.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using PetsDate.Data;
    using PetsDate.Web.ViewModels;
    using PetsDate.Web.ViewModels.Home;

    public class HomeController : BaseController
    {
        private readonly ApplicationDbContext db;

        public HomeController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var viewModel = new IndexViewModel
            {
                AnimalsCount = this.db.Animals.Count(),
                AnimalImagesCount = this.db.Images.Count(),
                ClinicCount = this.db.Clinics.Count(),
                ClinicImagesCount = this.db.ClinicImages.Count(),
                EventsCount = this.db.Events.Count(),
                HotelImagesCount = this.db.HotelImages.Count(),
                HoteslCount = this.db.Hotels.Count(),
                PublicationCount = this.db.Publications.Count(),
                SosSignslsCount = this.db.SosSignals.Count(),
                SosImagesCount = this.db.SosImages.Count(),
                CategoriesCount = this.db.Categories.Count(),
            };

            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
