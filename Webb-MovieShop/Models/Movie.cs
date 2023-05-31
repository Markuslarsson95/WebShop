using System.ComponentModel.DataAnnotations;

namespace Webb_MovieShop.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string ImgUrl { get; set; }

        public List<Actor_Movie> Actors_Movies { get; set; }
    }
}
