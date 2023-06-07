using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using Webb_MovieShop.Data;
using Webb_MovieShop.Models;
using static System.Net.WebRequestMethods;
using Humanizer;
using System.Drawing.Drawing2D;
using System.Net.NetworkInformation;

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
            //Kolla om admin, filmer eller prducenter finns i databasen
            if (!_context.Roles.Any(m => m.Name == "Admin") || !_context.Movies.Any() || !_context.Producers.Any())
            {
                //Anropa funktion för populate DB
                await FillDB();
            }
            return View();
        }

        public IActionResult About()
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
                {"Robert Zemeckis", "71", "https://m.media-amazon.com/images/M/MV5BMTgyMTMzMDUyNl5BMl5BanBnXkFtZTcwODA0ODMyMw@@._V1_.jpg"},
                {"James Cameron", "68", "https://www.themoviedb.org/t/p/w500/9NAZnTjBQ9WcXAQEzZpKy4vdQto.jpg" },
                {"David Fincher", "60", "https://m.media-amazon.com/images/M/MV5BMTc1NDkwMTQ2MF5BMl5BanBnXkFtZTcwMzY0ODkyMg@@._V1_FMjpg_UX1000_.jpg" },
                {"George Lucas", "79", "https://e1.pxfuel.com/desktop-wallpaper/588/986/desktop-wallpaper-george-lucas-art-artist-biographies-cgi-computer-en-george-lucas-thumbnail.jpg" },
                {"Phil Lord", "47", "https://m.media-amazon.com/images/M/MV5BOTI5NTUwMTAwOV5BMl5BanBnXkFtZTcwMDIxMDgzOQ@@._V1_.jpg" },
                {"Gavin O'Connor", "59", "https://tr.web.img4.acsta.net/pictures/15/09/16/10/23/049714.jpg" },
                {"Quentin Tarantino", "60", "https://cdn.britannica.com/81/220481-050-55413025/Quentin-Tarantino-2020.jpg" }
            };
            for (int i = 0; i < 9; i++)
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
                "https://posters.movieposterdb.com/10_06/2010/1375666/l_1375666_07030c72.jpg", "0"},
                {"Avatar", "Adventure", "A paraplegic Marine dispatched to the moon Pandora on a unique mission becomes torn between following his orders and protecting the world he feels is his home.",
                "https://posters.movieposterdb.com/23_01/2010/1778212/l_capturing-avatar-movie-poster_5327bfa8.jpg", "3" },
                {"The Social Network", "Drama", "As Harvard student Mark Zuckerberg creates the social networking site that would become known as Facebook, he is sued by the twins who claimed he stole their idea and by the co-founder who was later squeezed out of the business."
                ,"https://posters.movieposterdb.com/10_09/2010/1285016/l_1285016_dfc017d5.jpg", "4" },
                {"Star Wars A New Hope", "Sci-Fi", "Luke Skywalker joins forces with a Jedi Knight, a cocky pilot, a Wookiee and two droids to save the galaxy from the Empire's world-destroying battle station, while also attempting to rescue Princess Leia from the mysterious Darth Vader.",
                 "https://posters.movieposterdb.com/21_01/1977/76759/l_76759_43e2730c.jpg", "5" },
                {"21 Jump Street", "Comedy", "A pair of underachieving cops are sent back to a local high school to blend in and bring down a synthetic drug ring.",
                "https://posters.movieposterdb.com/12_02/2012/1232829/l_1232829_aa4a3d65.jpg", "6"},
                {"Warrior", "Drama", "The youngest son of an alcoholic former boxer returns home, where he's trained by his father for competition in a mixed martial arts tournament - a path that puts the fighter on a collision course with his estranged, older brother.",
                "https://posters.movieposterdb.com/11_11/2011/1291584/l_1291584_3b14b0c2.jpg", "7"},
                {"Fight Club", "Drama", "An insomniac office worker and a devil-may-care soap maker form an underground fight club that evolves into much more.", 
                "https://posters.movieposterdb.com/05_09/1999/0137523/l_53787_0137523_7ccf70c6.jpg", "4" },
                {"The Terminator", "Action", "A human soldier is sent from 2029 to 1984 to stop an almost indestructible cyborg killing machine, sent from the same year, which has been programmed to execute a young woman whose unborn son is the key to humanity's future salvation.",
                "https://posters.movieposterdb.com/22_11/1991/301928/l_the-terminator-movie-poster_c32bbde4.jpg", "3" },
                {"Back To The Future", "Adventure", "Marty McFly, a 17-year-old high school student, is accidentally sent 30 years into the past in a time-traveling DeLorean invented by his close friend, the maverick scientist Doc Brown.",
                "https://posters.movieposterdb.com/23_01/1989/2197817/l_back-to-the-future-movie-poster_c8254646.jpg", "2"},
                {"King Kong", "Action", "A greedy film producer assembles a team of moviemakers and sets out for the infamous Skull Island, where they find more than just cannibalistic natives.",
                "https://posters.movieposterdb.com/05_11/2005/0360717/l_68663_0360717_506989dd.jpg", "1"},
                {"Inglourious Basterds", "War", "In Nazi-occupied France during World War II, a plan to assassinate Nazi leaders by a group of Jewish U.S. soldiers coincides with a theatre owner's vengeful plans for the same.",
                "https://posters.movieposterdb.com/22_12/2009/361748/l_inglourious-basterds-movie-poster_10cbca6a.jpg", "8"}
            };
            //Sparar ner producers till en lista
            List<Producer> producers = await _context.Producers.ToListAsync();
            for (int i = 0; i < 14; i++)
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