using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace csharp_server_sent_events.Controllers
{
    public class RandomController : Controller
    {
        private static Random random = new Random();

        [Route("Random")]
        public async Task Index()
        {
            var utf8 = new UTF8Encoding();
            Response.Headers.Add("Content-Type", "text/event-stream");

            while (true) {
                var data = new {
                    random = random.Next(0, 100),
                    created = DateTime.Now,
                };

                await Response.Body.WriteAsync(utf8.GetBytes("event: random\n"));
                await Response.Body.WriteAsync(utf8.GetBytes("data: " + JsonSerializer.Serialize(data) + "\n\n"));
                await Response.Body.FlushAsync();

                await Task.Delay(random.Next(0, 10000));
            }
        }
    }
}
