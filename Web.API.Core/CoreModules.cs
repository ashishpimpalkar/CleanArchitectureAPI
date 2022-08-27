using Autofac;
using Autofac.Features.AttributeFilters;
using System;
using System.Collections.Generic;
using System.Text;
using Web.API.Core.Integrators;
using Web.API.Core.Interfaces;

namespace Web.API.Core
{
    public class CoreModules:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmployeeIntegrator>().Named<IIntegrator>("Employee").WithAttributeFiltering().PropertiesAutowired();
       
        }
    }
}
