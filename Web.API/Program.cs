using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Web.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Infrastructure.Logging.LoggerCofiguration.Configuration())
                .CreateLogger();

            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(Infrastructure.Logging.LoggerCofiguration.Configuration())
                .ConfigureLogging(log=> { Infrastructure.Logging.LoggerCofiguration.Configure(log); })
                .UseStartup<Startup>();
    }
}
