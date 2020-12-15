namespace PetsDate.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using PetsDate.Data;
    using PetsDate.Data.Common.Repositories;
    using PetsDate.Data.Models;

    [Area("Administration")]
    public class ClinicsController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IDeletableEntityRepository<Clinic> dataRepository;

        public ClinicsController(
            ApplicationDbContext context,
            IDeletableEntityRepository<Clinic> dataRepository)
        {
            this.db = context;
            this.dataRepository = dataRepository;
        }

        // GET: Administration/Clinics
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.db.Clinics.Include(c => c.User);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/Clinics/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var clinic = await this.db.Clinics
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clinic == null)
            {
                return this.NotFound();
            }

            return this.View(clinic);
        }

        // GET: Administration/Clinics/Create
        public IActionResult Create()
        {
            this.ViewData["UserId"] = new SelectList(this.db.Users, "Id", "Id");
            return this.View();
        }

        // POST: Administration/Clinics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Location,ImageUrl,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Clinic clinic)
        {
            if (this.ModelState.IsValid)
            {
                this.db.Add(clinic);
                await this.db.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["UserId"] = new SelectList(this.db.Users, "Id", "Id", clinic.UserId);
            return this.View(clinic);
        }

        // GET: Administration/Clinics/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var clinic = await this.db.Clinics.FindAsync(id);
            if (clinic == null)
            {
                return this.NotFound();
            }

            this.ViewData["UserId"] = new SelectList(this.db.Users, "Id", "Id", clinic.UserId);
            return this.View(clinic);
        }

        // POST: Administration/Clinics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Location,ImageUrl,UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Clinic clinic)
        {
            if (id != clinic.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.db.Update(clinic);
                    await this.db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.ClinicExists(clinic.Id))
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

            this.ViewData["UserId"] = new SelectList(this.db.Users, "Id", "Id", clinic.UserId);
            return this.View(clinic);
        }

        // GET: Administration/Clinics/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var clinic = await this.db.Clinics
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clinic == null)
            {
                return this.NotFound();
            }

            return this.View(clinic);
        }

        // POST: Administration/Clinics/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var clinic = this.dataRepository.All().FirstOrDefault(x => x.Id == id);
            this.dataRepository.Delete(clinic);
            await this.dataRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool ClinicExists(string id)
        {
            return this.db.Clinics.Any(e => e.Id == id);
        }
    }
}
