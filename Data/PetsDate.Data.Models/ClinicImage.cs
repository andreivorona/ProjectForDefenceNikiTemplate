namespace PetsDate.Data.Models
{
    using System;

    using PetsDate.Data.Common.Models;

    public class ClinicImage : BaseModel<string>
    {
        public ClinicImage()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string ClinicId { get; set; }

        public virtual Clinic Clinic { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string Extension { get; set; }
    }
}
