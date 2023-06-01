
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
                        Name = "Christopher Nolan",
                        Age = 52
                    },
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
                    }
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