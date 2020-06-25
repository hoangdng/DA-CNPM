using Microsoft.Extensions.Hosting;
using SemanticWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Routing;

namespace SemanticWeb.Controllers
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
            long due_time;
            DateTime time_now = DateTime.Now;
            DateTime time_send = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, 10, 42, 0);
            if (TimeSpan.Compare(time_now.TimeOfDay, time_send.TimeOfDay) == -1)
            {
                due_time = (long)time_send.Subtract(time_now).TotalSeconds;
            }
            else
            {
                DateTime tomorrow = DateTime.Today.AddDays(1);
                time_send = new DateTime(tomorrow.Year, tomorrow.Month, tomorrow.Day, 10, 42, 0);
                due_time = (long)time_send.Subtract(time_now).TotalSeconds;
            }
            _timer = new Timer(SendEmail, null, due_time * 1000, 360000 * 24);

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

                        string content = string.Format("Có {0} bài viết mới. <br/> <a href=\"https://localhost:44331/Home/Unsubcribe/?mailid={1}\">Link</a>", list_id.Count, mail);

                        message.From.Add(new MailboxAddress("huutaibk", "huutaibkdn@gmail.com"));
                        message.To.Add(new MailboxAddress("user", mail));
                        message.Subject = "Subject";

                        message.Body = new TextPart("html")
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