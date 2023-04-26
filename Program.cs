using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace aspnet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = CreateHostBuilder(args);
            var host = builder.Build();
            host.Run();    
           
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var builder = new WebHostBuilder();

            var portVar = Environment.GetEnvironmentVariable("PORT");
            if (portVar is { Length: > 0 } && int.TryParse(portVar, out int port))
            {
                return Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        portVar = "5000";
                        webBuilder.UseStartup<Startup>();
                        webBuilder.UseUrls("https://localhost:" + portVar);
                    });

            } else
            {


                return Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                        webBuilder.UseUrls("https://*:443");
                    });
            }

        }
    }
}
