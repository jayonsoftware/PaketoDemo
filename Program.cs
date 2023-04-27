using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace aspnet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
  // Add console (Sink) as logging target
  .WriteTo.Console()

    // Set default minimum log level
    .MinimumLevel.Debug()

    // Create the actual logger
    .CreateLogger();

            var builder = CreateHostBuilder(args);

          



            builder.UseSerilog(Log.Logger);



            var host = builder.Build();


            host.Run();

 

        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {


            Log.Information(Environment.MachineName);
            var builder = new WebHostBuilder();

            var portVar = Environment.GetEnvironmentVariable("PORT");
            if (portVar is { Length: > 0 } && int.TryParse(portVar, out int port))
            {
                return Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {          
                        webBuilder.UseStartup<Startup>();
                    });

            } else
            { 

            return Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                        webBuilder.UseUrls("https://*:443", "http://*:80");
                    });
            }

        }
    }
}
