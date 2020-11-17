namespace PetsDate.Data.Models
{
    using System;

    using PetsDate.Data.Common.Models;

    public class HotelImage : BaseModel<string>
    {
        public HotelImage()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string HotelId { get; set; }

        public virtual Hotel Hotel { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string Extension { get; set; }
    }
}
