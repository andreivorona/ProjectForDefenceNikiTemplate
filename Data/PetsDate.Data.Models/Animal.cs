namespace PetsDate.Data.Models
{
    using System.Collections.Generic;

    using PetsDate.Data.Common.Models;

    public class Animal : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string Color { get; set; }

        public double Weight { get; set; }

        // one-many category
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        // connection with user table
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string ImageUrl { get; set; }

        public virtual ICollection<AnimalImage> AnimalImages { get; set; } = new HashSet<AnimalImage>();
    }
}
