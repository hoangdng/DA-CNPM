using Microsoft.Extensions.Hosting;
using PetWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Routing;

namespace PetWeb.Controllers
{
    public class SubscribeService : IHostedService, IDisposable
    {
   

        private readonly IServiceScopeFactory _scopeFactory;
        private Timer _timer;
        
        public SubscribeService(IServiceScopeFactory scopeFactory, IHttpContextAccessor context)
        {
            _scopeFactory = scopeFactory;

        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            DateTime time_now = DateTime.Now;
            DateTime time_send = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 12, 59, 0);
            long due_time =  (long) time_send.Subtract(time_now).TotalSeconds;
            if (due_time <= 0)
            {
                DateTime tomorrow = DateTime.Today.AddDays(1);
                time_send = new DateTime(tomorrow.Year, DateTime.Today.Month, DateTime.Today.Day, 12, 55, 0);
            }   
            _timer = new Timer(SendEmail, null, due_time*1000, 360000*24);
           
            return Task.CompletedTask;
        }

        public void SendEmail(object state)
        {   

            using (var scope = _scopeFactory.CreateScope())
            {

               
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var emails = context.Subscribers.Select(s => s.Email).ToList();

               

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 465, true);

                    //SMTP server authentication if needed
                    client.Authenticate("huutaibkdn@gmail.com", "huutai123456");

                    foreach (var mail in emails)
                    {
                        var message = new MimeMessage();
                        var post_id = context.Posts.Select(p => p.Id);
                        var list_id = post_id.ToList();

                        string content = string.Format("Có {0} bài viết mới. \n", list_id.Count);
                        
                        message.From.Add(new MailboxAddress("huutaibk", "huutaibkdn@gmail.com"));
                        message.To.Add(new MailboxAddress("user", mail));
                        message.Subject = "Subject";

                        message.Body = new TextPart("plain")
                        {
                            Text = content
                        };




                        client.Send(message);


                    }

                    client.Disconnect(true);
                }
            }
            

        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
