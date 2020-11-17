namespace PetsDate.Data.Models
{
    using System;
    using System.Collections.Generic;

    using PetsDate.Data.Common.Models;

    public class Clinic : BaseDeletableModel<string>
    {
        public Clinic()
        {
            this.Id = Guid.NewGuid().ToString();
            this.ClinicImages = new HashSet<ClinicImage>();
        }

        public string Name { get; set; }

        public string Location { get; set; }

        public ICollection<ClinicImage> ClinicImages { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
