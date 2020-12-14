namespace PetsDate.Data.Models
{
    using PetsDate.Data.Common.Models;

    public class Vote : BaseModel<int>
    {
        public int AnimalId { get; set; }

        public virtual Animal Animal { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int Value { get; set; }
    }
}
