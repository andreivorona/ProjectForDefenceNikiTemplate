namespace PetsDate.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using PetsDate.Data.Models;

    public class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            List<string> categories = new List<string>();
            categories.Add("Fishes");
            categories.Add("Rodents");
            categories.Add("Amphibians");
            categories.Add("Reptiles");
            categories.Add("Birds");
            categories.Add("Mammals");

            if (dbContext.Categories.Any())
            {
                return;
            }

            foreach (var category in categories)
            {
                await dbContext.Categories.AddAsync(new Category
                {
                    Name = category,
                });
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
