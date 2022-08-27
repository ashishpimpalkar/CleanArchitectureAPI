using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.AttributeMetadata;
using Autofac.Features.AttributeFilters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Web.API.Core;
using Web.API.Infrastructure;
using Web.API.Infrastructure.Data;

namespace Web.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            string allowedOrigins;

            services.AddDbContext<CleanContext>(options => options.UseSqlServer(Configuration.GetConnectionString("GeneralDB")));

            allowedOrigins = Configuration.GetValue<string>("AppSettings:AllowedOriginLocal");
            
            services.AddCors(options => {
                options.AddPolicy("AllowAnyOrigin",
                   
                    b => b.WithOrigins(allowedOrigins).AllowAnyMethod().AllowCredentials().AllowAnyHeader()

                    );

            });

            services.Configure<MvcOptions>(options=>{
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAnyOrigin"));

            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = Configuration["Jwt:Issuer"],
                   ValidAudience = Configuration["Jwt:Issuer"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
               };
           });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            //Add Dependency

            services.AddSwaggerGen(c => {
                c.SwaggerDoc(name: "v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "v1" });
            });
            var builder = new ContainerBuilder();
            builder.RegisterModule(new CoreModules());
            builder.RegisterModule(new InfrastructureModule());
            builder.RegisterModule<AttributedMetadataModule>();

            services.AddMvc().AddControllersAsServices();

            builder.Populate(services);

            var controllers = typeof(Startup).Assembly.GetTypes().Where(t => t.BaseType == typeof(ControllerBase)).ToArray();
            builder.RegisterTypes(controllers).WithAttributeFiltering();

            var container = builder.Build();

            return new AutofacServiceProvider(container);

            
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c=>{
                c.SwaggerEndpoint(url: "/swagger/v1/swagger.json",name:"My API V1");
            });
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
