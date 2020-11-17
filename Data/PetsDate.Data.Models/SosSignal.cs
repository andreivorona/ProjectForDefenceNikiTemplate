namespace PetsDate.Data.Models
{
    using System;
    using System.Collections.Generic;

    using PetsDate.Data.Common.Models;

    public class SosSignal : BaseDeletableModel<string>
    {
        public SosSignal()
        {
            this.Id = Guid.NewGuid().ToString();
            this.SosImages = new HashSet<SosImage>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<SosImage> SosImages { get; set; }
    }
}
