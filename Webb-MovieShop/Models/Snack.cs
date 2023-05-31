using System.ComponentModel.DataAnnotations;

namespace Webb_MovieShop.Models
{
    public class Snack
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
