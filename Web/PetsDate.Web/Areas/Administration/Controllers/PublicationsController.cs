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
    public class PublicationsController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IDeletableEntityRepository<Publication> dataRepository;

        public PublicationsController(
            ApplicationDbContext context,
            IDeletableEntityRepository<Publication> dataRepository)
        {
            this.db = context;
            this.dataRepository = dataRepository;
        }

        // GET: Administration/Publications
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.db.Publications.Include(p => p.User);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/Publications/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var publication = await this.db.Publications
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publication == null)
            {
                return this.NotFound();
            }

            return this.View(publication);
        }

        // GET: Administration/Publications/Create
        public IActionResult Create()
        {
            this.ViewData["UserId"] = new SelectList(this.db.Users, "Id", "Id");
            return this.View();
        }

        // POST: Administration/Publications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Publication publication)
        {
            if (this.ModelState.IsValid)
            {
                this.db.Add(publication);
                await this.db.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["UserId"] = new SelectList(this.db.Users, "Id", "Id", publication.UserId);
            return this.View(publication);
        }

        // GET: Administration/Publications/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var publication = await this.db.Publications.FindAsync(id);
            if (publication == null)
            {
                return this.NotFound();
            }

            this.ViewData["UserId"] = new SelectList(this.db.Users, "Id", "Id", publication.UserId);
            return this.View(publication);
        }

        // POST: Administration/Publications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Description,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Publication publication)
        {
            if (id != publication.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.db.Update(publication);
                    await this.db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.PublicationExists(publication.Id))
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

            this.ViewData["UserId"] = new SelectList(this.db.Users, "Id", "Id", publication.UserId);
            return this.View(publication);
        }

        // GET: Administration/Publications/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var publication = await this.db.Publications
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (publication == null)
            {
                return this.NotFound();
            }

            return this.View(publication);
        }

        // POST: Administration/Publications/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var publication = this.dataRepository.All().FirstOrDefault(x => x.Id == id);
            this.dataRepository.Delete(publication);
            await this.dataRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool PublicationExists(string id)
        {
            return this.db.Publications.Any(e => e.Id == id);
        }
    }
}
