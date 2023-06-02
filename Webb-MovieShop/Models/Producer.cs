using System.ComponentModel.DataAnnotations;

namespace Webb_MovieShop.Models
{
    public class Producer
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string PictureUrl { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
