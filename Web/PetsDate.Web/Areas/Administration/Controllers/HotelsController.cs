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
    public class HotelsController : Controller
    {
        private readonly ApplicationDbContext db;
        private readonly IDeletableEntityRepository<Hotel> dataRepository;

        public HotelsController(
            ApplicationDbContext context,
            IDeletableEntityRepository<Hotel> dataRepository)
        {
            this.db = context;
            this.dataRepository = dataRepository;
        }

        // GET: Administration/Hotels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.db.Hotels.Include(h => h.User);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/Hotels/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var hotel = await this.db.Hotels
                .Include(h => h.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hotel == null)
            {
                return this.NotFound();
            }

            return this.View(hotel);
        }

        // GET: Administration/Hotels/Create
        public IActionResult Create()
        {
            this.ViewData["UserId"] = new SelectList(this.db.Users, "Id", "Id");
            return this.View();
        }

        // POST: Administration/Hotels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Location,Description,UserId,ImageUrl,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Hotel hotel)
        {
            if (this.ModelState.IsValid)
            {
                this.db.Add(hotel);
                await this.db.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["UserId"] = new SelectList(this.db.Users, "Id", "Id", hotel.UserId);
            return this.View(hotel);
        }

        // GET: Administration/Hotels/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var hotel = await this.db.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return this.NotFound();
            }

            this.ViewData["UserId"] = new SelectList(this.db.Users, "Id", "Id", hotel.UserId);
            return this.View(hotel);
        }

        // POST: Administration/Hotels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Location,Description,UserId,ImageUrl,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.db.Update(hotel);
                    await this.db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.HotelExists(hotel.Id))
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

            this.ViewData["UserId"] = new SelectList(this.db.Users, "Id", "Id", hotel.UserId);
            return this.View(hotel);
        }

        // GET: Administration/Hotels/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var hotel = await this.db.Hotels
                .Include(h => h.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hotel == null)
            {
                return this.NotFound();
            }

            return this.View(hotel);
        }

        // POST: Administration/Hotels/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var hotel = this.dataRepository.All().FirstOrDefault(x => x.Id == id);
            this.dataRepository.Delete(hotel);
            await this.dataRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool HotelExists(string id)
        {
            return this.db.Hotels.Any(e => e.Id == id);
        }
    }
}
