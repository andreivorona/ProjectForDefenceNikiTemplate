﻿namespace PetsDate.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PetsDate.Services.Data;
    using PetsDate.Web.ViewModels;
    using PetsDate.Web.ViewModels.Animal;

    public class HomeController : BaseController
    {
        private readonly IGetCountsService countsService;
        private readonly IAnimalService animalService;

        public HomeController(
            IGetCountsService countsService,
            IAnimalService animalService)
        {
            this.countsService = countsService;
            this.animalService = animalService;
        }

        public IActionResult Index()
        {
            var viewModel = this.countsService.GetCounts();
            viewModel.Users = this.countsService.GetAll().ToList();
            viewModel.Animals = this.animalService.GetAllHomePage();

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Info(int id)
        {
            var viewModel = this.animalService.GetInfoHomePage(id);

            return this.View(viewModel);
        }

        public IActionResult ImagesCollection(int id)
        {
            var viewModel = new AnimalListViewModel
            {
                AnimalImages = this.animalService.GetAnimalImagesHomePage(id),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Chat()
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
