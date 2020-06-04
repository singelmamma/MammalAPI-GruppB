using MammalAPI.Context;
using MammalAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Microsoft.OpenApi.Models;
using MammalAPI.Authentication;

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
                c.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "MammalApi", Version = "v1", Description ="The great Mammal Api" });
                c.OperationFilter<HeaderParameter>();
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

            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint(url: "/swagger/v1/swagger.json", name: "MammalApi");
            });
        }
    }
}
