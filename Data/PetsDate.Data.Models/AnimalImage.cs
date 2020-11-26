namespace PetsDate.Data.Models
{
    public class AnimalImage
    {
        public int AnimalId { get; set; }

        public virtual Animal Animal { get; set; }

        public string ImageId { get; set; }

        public virtual Image Image { get; set; }
    }
}
