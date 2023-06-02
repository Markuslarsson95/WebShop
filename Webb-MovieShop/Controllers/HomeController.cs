using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using Webb_MovieShop.Data;
using Webb_MovieShop.Models;
using static System.Net.WebRequestMethods;

namespace Webb_MovieShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public ApplicationDbContext _context { get; set; }
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //Kolla om admin roll finns till DB
            if (!_context.Roles.Any(m => m.Name == "Admin"))
            {
                //Anropa funktion för populate DB
                await FillDB();
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private async Task FillDB()
        {
            var roleStore = new RoleStore<IdentityRole>(_context);
            var userStore = new UserStore<IdentityUser>(_context);

            //Skapar roller om det inte finns i DB
            string[] roles = new string[] { "Admin", "User" };
            foreach (string role in roles)
            {
                //Skapa en role i DB med namnet från 'role' variabel
                roleStore.CreateAsync(new IdentityRole(role)).Wait();

                var newRole = _context.Roles.Where(m => m.Name == role).FirstOrDefault();
                newRole.NormalizedName = role.ToUpper();

                _context.Roles.Update(newRole);
                await _context.SaveChangesAsync();
            }

            //Skapa nya users
            string user = "Admin";
            string ePostHandler = "@gmail.com";

            //Skapa ett nytt IdentityUser objekt
            var newUser = new IdentityUser();
            newUser.UserName = user + ePostHandler;
            newUser.Email = user + ePostHandler;

            newUser.NormalizedUserName = (user + ePostHandler).ToUpper();
            newUser.NormalizedEmail = (user + ePostHandler).ToUpper();

            newUser.EmailConfirmed = true;

            string password = "12345";
            var hasher = new PasswordHasher<IdentityUser>();
            newUser.PasswordHash = hasher.HashPassword(newUser, password);

            //Lägg till användaren till DB
            userStore.CreateAsync(newUser).Wait();

            // Här lägger vi till användaren i "admin" rollen
            userStore.AddToRoleAsync(newUser, "Admin").Wait();

            await GenerateProducers();
            await GenerateMovies();
        }
        private async Task GenerateProducers()
        {
            string[,] producers =
            {
                {"Christopher Nolan", "52", "https://www.looper.com/img/gallery/rules-actors-have-to-follow-in-christopher-nolan-movies/l-intro-1652977075.jpg"},
                {"Peter Jackson", "61", "https://m.media-amazon.com/images/M/MV5BYjFjOThjMjgtYzM5ZS00Yjc0LTk5OTAtYWE4Y2IzMDYyZTI5XkEyXkFqcGdeQXVyMTMxMTIwMTE0._V1_FMjpg_UX1000_.jpg"},
                {"Robert Zemeckis", "71", "https://m.media-amazon.com/images/M/MV5BMTgyMTMzMDUyNl5BMl5BanBnXkFtZTcwODA0ODMyMw@@._V1_.jpg"}
            };
            for (int i = 0; i < 3; i++)
            {
                Producer producer = new Producer
                {
                    Name = producers[i, 0],
                    Age = int.Parse(producers[i, 1]),
                    PictureUrl = producers[i, 2]
                };
            _context.Producers.Add(producer);
            await _context.SaveChangesAsync();
            }
        }

        private async Task GenerateMovies()
        {
            string[,] movies =
            {
                {"The Dark Knight", "Action", "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice",
                "https://posters.movieposterdb.com/08_05/2008/468569/s_468569_f0e2cd63.jpg", "0"},
                {"The Lord of the Rings: The Return of the King" , "Adventure", "Gandalf and Aragorn lead the World of Men against Sauron's army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.",
                "https://posters.movieposterdb.com/04_12/2003/0167260/s_183_0167260_6815154e.jpg", "1"},
                {"Forrest Gump", "Drama", "The presidencies of Kennedy and Johnson, the Vietnam War, the Watergate scandal and other historical events unfold from the perspective of an Alabama man with an IQ of 75, whose only desire is to be reunited with his childhood sweetheart.",
                "https://posters.movieposterdb.com/05_06/1994/0109830/s_21293_0109830_af6ba7a1.jpg", "2"},
                {"Inception", "Sci-Fi" , "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O., but his tragic past may doom the project and his team to disaster.",
                "https://posters.movieposterdb.com/10_06/2010/1375666/l_1375666_07030c72.jpg", "0"}
            };
            //Sparar ner producers till en lista
            List<Producer> producers = await _context.Producers.ToListAsync();
            for (int i = 0; i < 4; i++)
            {
                Movie movie = new Movie
                {
                    Title = movies[i, 0],
                    Genre = movies[i, 1],
                    Description = movies[i, 2],
                    ImgUrl = movies[i, 3],
                    //Hämtar index position av listan med hjälp av movies ovan
                    Producer = producers[int.Parse(movies[i, 4])]
                };
                _context.Movies.Add(movie);
                await _context.SaveChangesAsync();
            }
        }
    }
}