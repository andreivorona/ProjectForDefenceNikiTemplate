namespace PetsDate.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using PetsDate.Data;
    using PetsDate.Data.Models;

    [Area("Administration")]
    public class AnimalImagesController : Controller
    {
        private readonly ApplicationDbContext db;

        public AnimalImagesController(ApplicationDbContext context)
        {
            this.db = context;
        }

        // GET: Administration/AnimalImages
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.db.AnimalImages.Include(a => a.Animal).Include(a => a.User);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/AnimalImages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var animalImage = await this.db.AnimalImages
                .Include(a => a.Animal)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animalImage == null)
            {
                return this.NotFound();
            }

            return this.View(animalImage);
        }

        // GET: Administration/AnimalImages/Create
        public IActionResult Create()
        {
            this.ViewData["AnimalId"] = new SelectList(this.db.Animals, "Id", "Id");
            this.ViewData["UserId"] = new SelectList(this.db.Users, "Id", "Id");
            return this.View();
        }

        // POST: Administration/AnimalImages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,AnimalId,ImageUrl,Id,CreatedOn,ModifiedOn")] AnimalImage animalImage)
        {
            if (this.ModelState.IsValid)
            {
                this.db.Add(animalImage);
                await this.db.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["AnimalId"] = new SelectList(this.db.Animals, "Id", "Id", animalImage.AnimalId);
            this.ViewData["UserId"] = new SelectList(this.db.Users, "Id", "Id", animalImage.UserId);
            return this.View(animalImage);
        }

        // GET: Administration/AnimalImages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var animalImage = await this.db.AnimalImages.FindAsync(id);
            if (animalImage == null)
            {
                return this.NotFound();
            }

            this.ViewData["AnimalId"] = new SelectList(this.db.Animals, "Id", "Id", animalImage.AnimalId);
            this.ViewData["UserId"] = new SelectList(this.db.Users, "Id", "Id", animalImage.UserId);
            return this.View(animalImage);
        }

        // POST: Administration/AnimalImages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,AnimalId,ImageUrl,Id,CreatedOn,ModifiedOn")] AnimalImage animalImage)
        {
            if (id != animalImage.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.db.Update(animalImage);
                    await this.db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.AnimalImageExists(animalImage.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["AnimalId"] = new SelectList(this.db.Animals, "Id", "Id", animalImage.AnimalId);
            this.ViewData["UserId"] = new SelectList(this.db.Users, "Id", "Id", animalImage.UserId);
            return this.View(animalImage);
        }

        // GET: Administration/AnimalImages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var animalImage = await this.db.AnimalImages
                .Include(a => a.Animal)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animalImage == null)
            {
                return this.NotFound();
            }

            return this.View(animalImage);
        }

        // POST: Administration/AnimalImages/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animalImage = await this.db.AnimalImages.FindAsync(id);
            this.db.AnimalImages.Remove(animalImage);
            await this.db.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool AnimalImageExists(int id)
        {
            return this.db.AnimalImages.Any(e => e.Id == id);
        }
    }
}
