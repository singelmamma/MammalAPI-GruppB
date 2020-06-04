using MammalAPI.Context;
using MammalAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using System;
using MammalAPI.Header;

namespace MammalAPI
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DBContext>();
            services.AddScoped<IMammalRepository, MammalRepository>();
            services.AddScoped<IHabitatRepository, HabitatRepository>();
            services.AddScoped<IFamilyRepository, FamilyRepository>();
            services.AddAutoMapper(typeof(Startup));
            services.AddMvc(options => options.EnableEndpointRouting=false).SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<AddCommonParameOperationFilter>();

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = $"The Great MammalAPI",
                    Description = "v1 API",
                    TermsOfService = new Uri("https://www.c-sharpcorner.com/members/catcher-wong"),
                    Contact = new OpenApiContact
                    {
                        Name = "Group B",
                        
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Apache-2.0",
                        Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
                    }
                });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "The Great MammalAPI v1.0");
            });
        }
    }
}
