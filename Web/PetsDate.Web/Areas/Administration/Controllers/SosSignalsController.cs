namespace PetsDate.Web.Areas.Administration.Controllers
{
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
    public class SosSignalsController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IDeletableEntityRepository<SosSignal> dataRepository;

        public SosSignalsController(
            ApplicationDbContext context,
            IDeletableEntityRepository<SosSignal> dataRepository)
        {
            this.db = context;
            this.dataRepository = dataRepository;
        }

        // GET: Administration/SosSignals
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.db.SosSignals.Include(s => s.User);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/SosSignals/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var sosSignal = await this.db.SosSignals
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sosSignal == null)
            {
                return this.NotFound();
            }

            return this.View(sosSignal);
        }

        // GET: Administration/SosSignals/Create
        public IActionResult Create()
        {
            this.ViewData["UserId"] = new SelectList(this.db.Users, "Id", "Id");
            return this.View();
        }

        // POST: Administration/SosSignals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,Location,UserId,ImageUrl,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] SosSignal sosSignal)
        {
            if (this.ModelState.IsValid)
            {
                this.db.Add(sosSignal);
                await this.db.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["UserId"] = new SelectList(this.db.Users, "Id", "Id", sosSignal.UserId);
            return this.View(sosSignal);
        }

        // GET: Administration/SosSignals/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var sosSignal = await this.db.SosSignals.FindAsync(id);
            if (sosSignal == null)
            {
                return this.NotFound();
            }

            this.ViewData["UserId"] = new SelectList(this.db.Users, "Id", "Id", sosSignal.UserId);
            return this.View(sosSignal);
        }

        // POST: Administration/SosSignals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Description,Location,UserId,ImageUrl,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] SosSignal sosSignal)
        {
            if (id != sosSignal.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.db.Update(sosSignal);
                    await this.db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.SosSignalExists(sosSignal.Id))
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

            this.ViewData["UserId"] = new SelectList(this.db.Users, "Id", "Id", sosSignal.UserId);
            return this.View(sosSignal);
        }

        // GET: Administration/SosSignals/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var sosSignal = await this.db.SosSignals
                .Include(s => s.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sosSignal == null)
            {
                return this.NotFound();
            }

            return this.View(sosSignal);
        }

        // POST: Administration/SosSignals/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var sosSignal = this.dataRepository.All().FirstOrDefault(x => x.Id == id);
            this.dataRepository.Delete(sosSignal);
            await this.dataRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool SosSignalExists(string id)
        {
            return this.db.SosSignals.Any(e => e.Id == id);
        }
    }
}
