namespace PetsDate.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using PetsDate.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            await dbContext.Categories.AddAsync(new Category
            {
                Name = "Dog",
            });

            await dbContext.Categories.AddAsync(new Category
            {
                Name = "Cat",
            });

            await dbContext.Categories.AddAsync(new Category
            {
                Name = "Other",
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
