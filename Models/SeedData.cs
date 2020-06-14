using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PetWeb.Data;
using System;
using System.Linq;


namespace PetWeb.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Animals.Any())
                {
                    return;
                }
                if (context.Categories.Any())
                {
                    return;
                }
                if (context.Cities.Any())
                {
                    return;
                }

                context.Animals.AddRange(
                    new Animal
                    {
                        Name = "cat",
                        Description = "Mèo"
                    },
                    new Animal
                    {
                        Name = "dog",
                        Description = "Chó"
                    },
                    new Animal
                    {
                        Name = "others",
                        Description = "Khác"
                    }
                );
                context.SaveChanges();

                context.Categories.AddRange(
                    new Category
                    {
                        Name = "adopt",
                        Description = "Nhận nuôi"
                    },
                    new Category
                    {
                        Name = "disease",
                        Description = "Bệnh tât"
                    },
                    new Category
                    {
                        Name = "help",
                        Description = "Giúp đỡ"
                    }
                );
                context.SaveChanges();

                context.Cities.AddRange(
                    new City
                    {
                        Name = "danang",
                        Description = "Đà Nẵng"
                    },
                    new City
                    {
                        Name = "hanoi",
                        Description = "Hà Nội"
                    },
                    new City
                    {
                        Name = "tphcm",
                        Description = "Tp.Hồ Chí Minh"
                    },
                    new City
                    {
                        Name = "others",
                        Description = "Khu vực khác"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}