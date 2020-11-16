namespace PetsDate.Data.Models
{
    using System.Collections.Generic;

    using PetsDate.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Animals = new HashSet<Animal>();
        }

        public string Name { get; set; }

        public virtual ICollection<Animal> Animals { get; set; }
    }
}
