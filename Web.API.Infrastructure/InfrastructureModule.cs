using Autofac;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Web.API.Core.Gateways;
using Web.API.Infrastructure.Data;
using Web.API.Infrastructure.Repository;

namespace Web.API.Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<>)).As(typeof(IGenericRepository<>));
            builder.RegisterType<CleanContext>().InstancePerLifetimeScope();
            //need to write for loggingReg
            builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>));

        }
    }
}
