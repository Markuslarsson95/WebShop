using System.ComponentModel.DataAnnotations;

namespace Webb_MovieShop.Models
{
    public class Actor_Movie
    {
        
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        
        public int ActorId { get; set; }
        public Actor Actor { get; set; }
    }
}
