using FightClubWebApp.Data.Enum;
using FightClubWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace FightClubWebApp.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

                context.Database.EnsureCreated();

                if (!context.Clubs.Any())
                {
                    context.Clubs.AddRange(new List<Club>()
                    {
                        new Club()
                        {
                            Title = "Boxing Club ",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "Old shcool boxing club, for everyone, who want to master an art of classic boxing",
                            ClubCategory = ClubCategory.Boxing,
                            Address = new Address()
                            {
                                Street = "Wielicka 23",
                                City = "Cracow",
                                State = "Malopolskie"
                            }
                         },
                        new Club()
                        {
                            Title = "UFC",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "UFC, Mixed martial arts club ",
                            ClubCategory = ClubCategory.UFC,
                            Address = new Address()
                            {
                                Street = "123 Main St",
                                City = "Bialystok",
                                State = "Podlaskie"
                            }
                        },
                        new Club()
                        {
                            Title = "Kick Boxing",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "Kick Boxing club, be like Chuck Norris",
                            ClubCategory = ClubCategory.KickBoxing,
                            Address = new Address()
                            {
                                Street = "Spoldzielcow 2",
                                City = "Cracow",
                                State = "Malopolskie"
                            }
                        },
                        new Club()
                        {
                            Title = "Street Fight",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "Old School Street Fights, you wanna get a fight just call us",
                            ClubCategory = ClubCategory.StreetFight,
                            Address = new Address()
                            {
                                Street = "Sienkiewicza 12",
                                City = "Bialystok",
                                State = "Podlaskie"
                            }
                        }
                    });
                    context.SaveChanges();
                }
                //Fights
                if (!context.Fights.Any())
                {
                    context.Fights.AddRange(new List<Fight>()
                    {
                        new Fight()
                        {
                            Title = "Boxing Sparring",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "Sparring with professional fighters",
                            FightCategory = FightCategory.Sparring,
                            Address = new Address()
                            {
                                Street = "12 Main St",
                                City = "Warsaw",
                                State = "Mazowieckie"
                            }
                        },
                        new Fight()
                        {
                            Title = "UFC Training",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            Description = "UFC Training on saturday",
                            FightCategory = FightCategory.Training,
                            AddressId = 3,
                            Address = new Address()
                            {
                                Street = "Spoldzielcow 2",
                                City = "Cracow",
                                State = "Malopolskie"
                            }
                        }
                    });
                    context.SaveChanges();
                }
            }
        }

        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "seminenkovs@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "VitaliiSeminenko",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "Spoldzielcoq",
                            City = "Krakow",
                            State = "Malopolskie",
                        }
                    };
                    await userManager.CreateAsync(newAdminUser, "Code@12345");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@google.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Address = new Address()
                        {
                            Street = "ul Wielicka",
                            City = "Krakow",
                            State = "Malopolskie"
                        }
                    };
                    await userManager.CreateAsync(newAppUser, "Code@123");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
