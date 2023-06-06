
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Webb_MovieShop.Data;

namespace Webb_MovieShop.Models
{
    public class FillDB
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var userStore = new UserStore<IdentityUser>(context);


                //Lägger till skådespelare i Db om det inte redan finns några
                if (context.Producers.Any())
                {
                    return;
                }
                context.Producers.AddRange(new List<Producer>()
                {
                    new Producer()
                    {
                        Name = "Christopher Nolan",
                        Age = 52
                    },
                    new Producer()
                    {
                        Name = "Peter Jackson",
                        Age = 61
                    },
                    new Producer()
                    {
                        Name = "Robert Zemeckis",
                        Age = 71
                    },
                    new Producer()
                    {
                        Name = "James Cameron",
                        Age = 68
                    },
                    new Producer()
                    {
                        Name = "David Fincher",
                        Age = 60
                    },
                    new Producer()
                    {
                        Name = "George Lucas",
                        Age = 79
                    },
                    new Producer()
                    {
                        Name = "Phil Lord",
                        Age = 47
                    },
                    new Producer()
                    {
                        Name = "Gavin O'Connor",
                        Age = 59
                    },
                    new Producer()
                    {
                        Name= "Quentin Tarantino",
                        Age = 60
                    }
                });
                context.SaveChanges();

                if (context.Movies.Any())
                { 
                    return; 
                }
                context.Movies.AddRange(new List<Movie>()
                    {
                    new Movie()
                    {
                        Title = "The Dark Knight",
                        Genre = "Action",
                        Description = "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
                        ImgUrl = "https://posters.movieposterdb.com/08_05/2008/468569/s_468569_f0e2cd63.jpg"
                    },

                    new Movie()
                    {
                        Title = "The Lord of the Rings: The Return of the King",
                        Genre = "Adventure",
                        Description = "Gandalf and Aragorn lead the World of Men against Sauron's army to draw his gaze from Frodo and Sam as they approach Mount Doom with the One Ring.",
                        ImgUrl = "https://posters.movieposterdb.com/04_12/2003/0167260/s_183_0167260_6815154e.jpg"
                    },

                    new Movie()
                    {
                        Title = "Forrest Gump",
                        Genre = "Drama",
                        Description = "The presidencies of Kennedy and Johnson, the Vietnam War, the Watergate scandal and other historical events unfold from the perspective of an Alabama man with an IQ of 75, whose only desire is to be reunited with his childhood sweetheart.",
                        ImgUrl = "https://posters.movieposterdb.com/05_06/1994/0109830/s_21293_0109830_af6ba7a1.jpg"
                    },

                    new Movie()
                    {
                        Title = "Inception",
                        Genre = "Sci-Fi",
                        Description = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a C.E.O., but his tragic past may doom the project and his team to disaster.",
                        ImgUrl = "https://posters.movieposterdb.com/10_06/2010/1375666/l_1375666_07030c72.jpg"
                    },


                     new Movie()
                     {
                         Title = "Avatar",
                         Genre = "Adventure",
                         Description = "A paraplegic Marine dispatched to the moon Pandora on a unique mission becomes torn between following his orders and protecting the world he feels is his home.",
                         ImgUrl = "https://posters.movieposterdb.com/23_01/2010/1778212/l_capturing-avatar-movie-poster_5327bfa8.jpg"
                     },

                     new Movie()
                     {
                         Title = "The Social Network",
                         Genre = "Drama",
                         Description = "As Harvard student Mark Zuckerberg creates the social networking site that would become known as Facebook, he is sued by the twins who claimed he stole their idea and by the co-founder who was later squeezed out of the business.",
                         ImgUrl = "https://posters.movieposterdb.com/10_09/2010/1285016/l_1285016_dfc017d5.jpg"
                     },

                     new Movie()
                     {
                         Title = "Star Wars A New Hope",
                         Genre = "Sci-Fi",
                         Description = "Luke Skywalker joins forces with a Jedi Knight, a cocky pilot, a Wookiee and two droids to save the galaxy from the Empire's world-destroying battle station, while also attempting to rescue Princess Leia from the mysterious Darth Vader.",
                         ImgUrl = "https://posters.movieposterdb.com/21_01/1977/76759/l_76759_43e2730c.jpg"
                     },

                     new Movie()
                     {
                         Title = "21 Jump Street",
                         Genre = "Comdey",
                         Description = "A pair of underachieving cops are sent back to a local high school to blend in and bring down a synthetic drug ring.",
                         ImgUrl = "https://posters.movieposterdb.com/12_02/2012/1232829/l_1232829_aa4a3d65.jpg"
                     },

                     new Movie()
                     {
                         Title = "Warrior",
                         Genre = "Drama",
                         Description = "The youngest son of an alcoholic former boxer returns home, where he's trained by his father for competition in a mixed martial arts tournament - a path that puts the fighter on a collision course with his estranged, older brother.",
                         ImgUrl = "https://posters.movieposterdb.com/11_11/2011/1291584/l_1291584_3b14b0c2.jpg"
                     },

                     new Movie()
                     {
                         Title = "Fight Club",
                         Genre = "Drama",
                         Description = "An insomniac office worker and a devil-may-care soap maker form an underground fight club that evolves into much more.",
                         ImgUrl = "https://posters.movieposterdb.com/05_09/1999/0137523/l_53787_0137523_7ccf70c6.jpg"
                     },

                     new Movie()
                     {
                         Title = "The Terminator",
                         Genre = "Action",
                         Description = "A human soldier is sent from 2029 to 1984 to stop an almost indestructible cyborg killing machine, sent from the same year, which has been programmed to execute a young woman whose unborn son is the key to humanity's future salvation.",
                         ImgUrl = "https://posters.movieposterdb.com/22_11/1991/301928/l_the-terminator-movie-poster_c32bbde4.jpg"
                     },

                     new Movie()
                     {
                         Title = "Back To The Future",
                         Genre = "Adventure",
                         Description = "Marty McFly, a 17-year-old high school student, is accidentally sent 30 years into the past in a time-traveling DeLorean invented by his close friend, the maverick scientist Doc Brown.",
                         ImgUrl = "https://posters.movieposterdb.com/23_01/1989/2197817/l_back-to-the-future-movie-poster_c8254646.jpg"
                     },

                     new Movie()
                     {
                         Title = "King Kong",
                         Genre = "Action",
                         Description = "A greedy film producer assembles a team of moviemakers and sets out for the infamous Skull Island, where they find more than just cannibalistic natives.",
                         ImgUrl = "https://posters.movieposterdb.com/05_11/2005/0360717/l_68663_0360717_506989dd.jpg"
                     },

                     new Movie()
                     {
                         Title = "Inglourious Basterds",
                         Genre = "War",
                         Description = "In Nazi-occupied France during World War II, a plan to assassinate Nazi leaders by a group of Jewish U.S. soldiers coincides with a theatre owner's vengeful plans for the same.",
                         ImgUrl = "https://posters.movieposterdb.com/22_12/2009/361748/l_inglourious-basterds-movie-poster_10cbca6a.jpg"
                     },

                     });
                context.SaveChanges();

                if (context.Roles.Any())
                {
                    return;
                }
                //Skapar roller om det inte finns i DB
                string[] roles = new string[] { "Admin", "User" };
                foreach (string role in roles)
                {
                    //Skapa en role i DB med namnet från 'role' variabel
                    roleStore.CreateAsync(new IdentityRole(role)).Wait();

                    var newRole = context.Roles.Where(m => m.Name == role).FirstOrDefault();
                    newRole.NormalizedName = role.ToUpper();

                    context.Roles.Update(newRole);
                }
                if(context.Users.Any())
                {
                    return;
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
                
                context.SaveChanges();
            }
        }
    }
}