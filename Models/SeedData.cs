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

                context.Animals.AddRange(
                    new Animal
                    {
                        Name = "cat"
                    },
                    new Animal
                    {
                        Name = "dog"
                    },
                    new Animal
                    {
                        Name = "other"
                    }
                );
                context.SaveChanges();

                context.Categories.AddRange(
                    new Category
                    {
                        Name = "adopt"
                    },
                    new Category
                    {
                        Name = "disease"
                    },
                    new Category
                    {
                        Name = "help"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}