namespace PetsDate.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PetsDate.Web.ViewModels.Animal;

    public class AnimalController : Controller
    {
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreateAnimalInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            // create animal using service method
            // todo redirect to animal info page
            return this.Redirect("/");
        }
    }
}
