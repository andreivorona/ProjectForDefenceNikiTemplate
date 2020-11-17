namespace PetsDate.Data.Models
{
    using System;

    using PetsDate.Data.Common.Models;

    public class Event : BaseDeletableModel<string>
    {
        public Event()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public string Location { get; set; }

        public DateTime BeginEvent { get; set; }

        public DateTime EndEvent { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
