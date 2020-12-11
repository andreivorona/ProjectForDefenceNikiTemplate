namespace PetsDate.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PetsDate.Common;
    using PetsDate.Services.Data;
    using PetsDate.Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class EditController : BaseController
    {
        private readonly IGetCountsService countsService;

        public EditController(IGetCountsService countsService)
        {
            this.countsService = countsService;
        }

        public IActionResult Index()
        {
            var viewModel = this.countsService.GetCounts();

            return this.View(viewModel);
        }
    }
}
