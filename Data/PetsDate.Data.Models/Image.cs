namespace PetsDate.Data.Models
{
    using System;
    using System.Collections.Generic;
    using PetsDate.Data.Common.Models;

    public class Image : BaseModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public int AnimalId { get; set; }

        public virtual Animal Animal { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string Extension { get; set; }

        // safe in file system

        public virtual ICollection<AnimalImage> Animals { get; set; } = new HashSet<AnimalImage>();
    }
}
