using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Web.API.Infrastructure.Repository;
using Web.API.Core.Gateways;
using Web.API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Web.API.Core.Integrators;

namespace Web.API.Core.Test
{
    public static class BootStrap
    {
        static public IConfigurationRoot Configuration { get; set; }
        static public IContainer Container { get; set; }

        public static void Initialize()
        {
            var builder = new ContainerBuilder();
            builder.RegisterGeneric(typeof(GenericRepository<>))
                .As(typeof(IGenericRepository<>));
            builder.RegisterType<CleanContext>().InstancePerLifetimeScope();

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = configurationBuilder.Build();
            builder.Register(c =>
            {
                var opt = new DbContextOptionsBuilder<CleanContext>();
                opt.UseSqlServer(Configuration.GetConnectionString(""));
                return new CleanContext(opt.Options);

            }).AsSelf().InstancePerLifetimeScope();

            builder.Register<ILogger>((c, i)=>
                {
                    return new Mock<Logger<BaseIntegrator>>().Object;
            });

            Container = builder.Build();
        }
    }
}
