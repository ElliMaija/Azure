using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CrudWebApi.Filters;
using CrudWebApi.Formatters;
using DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace CrudWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                });
            }
            );
            //services.AddDbContext<MySqlContext>();
            services.AddMvc(options =>
            {
                //options.Filters.Add(typeof(MyTimerFilterAttribute));
                options.OutputFormatters.Add(new MyCsvOutputFormatter());
                options.FormatterMappings.SetMediaTypeMappingForFormat("csv", "text/csv");
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddXmlSerializerFormatters();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "My API",
                    Version = "v1",
                    Description = "Testing Swagger",
                    Contact = new Contact
                    {
                        Email = "joku@jossain.fi",
                        Name = "Admin"
                    }
                });
                var xmlPath = Path.Combine(AppContext.BaseDirectory, "Entities.xml");
                c.IncludeXmlComments(xmlPath);
                xmlPath = Path.Combine(AppContext.BaseDirectory, "CrudWebApi.xml");
                c.IncludeXmlComments(xmlPath);
                services.AddDbContext<MySqlContext>(options =>
                options.UseSqlServer(Configuration
                .GetConnectionString("CustomerDb")
                ));

            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}
