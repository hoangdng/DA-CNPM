using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SemanticWeb.Data;
using System;
using System.Linq;


namespace SemanticWeb.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Areas.Any())
                {
                    return;
                }

                context.Areas.AddRange(
                    new Area
                    {
                        Name = "haichau",
                        Description = "Hải Châu"
                    },
                    new Area
                    {
                        Name = "thanhkhe",
                        Description="Thanh Khê"
                    },
                    new Area
                    {
                        Name = "sontra",
                        Description = "Sơn Trà"
                    },
                    new Area
                    {
                        Name = "lienchieu",
                        Description = "Liên Chiểu"
                    },
                    new Area
                    {
                        Name = "camle",
                        Description = "Cẩm Lệ"
                    },
                    new Area
                    {
                        Name = "hoavang",
                        Description = "Hòa Vang"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}