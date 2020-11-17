namespace PetsDate.Data.Models
{
    using System;

    using PetsDate.Data.Common.Models;

    public class Publication : BaseDeletableModel<string>
    {
        public Publication()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Description { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
