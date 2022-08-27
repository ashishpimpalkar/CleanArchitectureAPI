using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Web.API.Infrastructure.Logging
{
    public static class LoggerCofiguration
    {
        public static IConfiguration Configuration()
        {
            {
                var builder = new ConfigurationBuilder();
                builder.AddJsonFile($"appsettings.json", false, true);
                return builder.Build();
            }
           

        }

        public static ILoggingBuilder Configure(ILoggingBuilder log)
        {
            log.AddSerilog(Log.Logger);
            return log;
        }
    }
}
