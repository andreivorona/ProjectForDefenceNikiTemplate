namespace PetsDate.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using PetsDate.Common;
    using PetsDate.Web.Controllers;

    [Area("Administration")]
    public class EditController : BaseController
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
