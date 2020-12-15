namespace PetsDate.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using PetsDate.Common;
    using PetsDate.Data;
    using PetsDate.Data.Common.Repositories;
    using PetsDate.Data.Models;

    [Area("Administration")]
    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class AnimalsController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IDeletableEntityRepository<Animal> dataRepository;

        public AnimalsController(
            ApplicationDbContext context,
            IDeletableEntityRepository<Animal> dataRepository)
        {
            this.db = context;
            this.dataRepository = dataRepository;
        }

        // GET: Administration/Animals
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.db.Animals.Include(a => a.Category).Include(a => a.User);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/Animals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var animal = await this.db.Animals
                .Include(a => a.Category)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animal == null)
            {
                return this.NotFound();
            }

            return this.View(animal);
        }

        // GET: Administration/Animals/Create
        public IActionResult Create()
        {
            this.ViewData["CategoryId"] = new SelectList(this.db.Categories, "Id", "Id");
            this.ViewData["UserId"] = new SelectList(this.db.Users, "Id", "Id");
            return this.View();
        }

        // POST: Administration/Animals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Age,Color,Weight,CategoryId,UserId,ImageUrl,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Animal animal)
        {
            if (this.ModelState.IsValid)
            {
                this.db.Add(animal);
                await this.db.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["CategoryId"] = new SelectList(this.db.Categories, "Id", "Id", animal.CategoryId);
            this.ViewData["UserId"] = new SelectList(this.db.Users, "Id", "Id", animal.UserId);
            return this.View(animal);
        }

        // GET: Administration/Animals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var animal = await this.db.Animals.FindAsync(id);
            if (animal == null)
            {
                return this.NotFound();
            }

            this.ViewData["CategoryId"] = new SelectList(this.db.Categories, "Id", "Id", animal.CategoryId);
            this.ViewData["UserId"] = new SelectList(this.db.Users, "Id", "Id", animal.UserId);
            return this.View(animal);
        }

        // POST: Administration/Animals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Age,Color,Weight,CategoryId,UserId,ImageUrl,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Animal animal)
        {
            if (id != animal.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.db.Update(animal);
                    await this.db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.AnimalExists(animal.Id))
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

            this.ViewData["CategoryId"] = new SelectList(this.db.Categories, "Id", "Id", animal.CategoryId);
            this.ViewData["UserId"] = new SelectList(this.db.Users, "Id", "Id", animal.UserId);
            return this.View(animal);
        }

        // GET: Administration/Animals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var animal = await this.db.Animals
                .Include(a => a.Category)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animal == null)
            {
                return this.NotFound();
            }

            return this.View(animal);
        }

        // POST: Administration/Animals/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animal = this.dataRepository.All().FirstOrDefault(x => x.Id == id);
            this.dataRepository.Delete(animal);
            await this.dataRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool AnimalExists(int id)
        {
            return this.db.Animals.Any(e => e.Id == id);
        }
    }
}
