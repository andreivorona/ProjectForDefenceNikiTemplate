namespace PetsDate.Data.Models
{
    using System;
    using System.Collections.Generic;

    using PetsDate.Data.Common.Models;

    public class Hotel : BaseDeletableModel<string>
    {
        public Hotel()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string ImageUrl { get; set; }
    }
}
