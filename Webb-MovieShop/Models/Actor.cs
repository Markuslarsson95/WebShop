using System.ComponentModel.DataAnnotations;

namespace Webb_MovieShop.Models
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public List<Actor_Movie> Actors_Movies { get; set; }
    }
}
