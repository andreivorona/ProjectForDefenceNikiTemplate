namespace PetsDate.Data.Models
{
    using System;

    using PetsDate.Data.Common.Models;

    public class SosImage : BaseModel<string>
    {
        public SosImage()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string SosSignalId { get; set; }

        public virtual SosSignal SosSignal { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string Extension { get; set; }
    }
}
