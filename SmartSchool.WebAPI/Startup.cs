using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SmartSchool.WebAPI.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace SmartSchool.WebAPI
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
            services.AddDbContext<Data.ProjContext>(
                options => options.UseSqlite(Configuration.GetConnectionString("Default"))
            );

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IRepositorio, Repositorio>();
            
            services.AddControllers()
                    .AddNewtonsoftJson(
                        options => options.SerializerSettings.ReferenceLoopHandling = 
                                   Newtonsoft.Json.ReferenceLoopHandling.Ignore );

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Example Web Api with Swagger",
                    Description = "It is only one example who use swagger in dotnet core. :)",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Júnior Porfirio",
                        Url = new Uri("https://www.medium.com/@juniorporfirio")
                    }
                });
            });            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.RoutePrefix = "swagger";
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Example");
            });
        }
    }
}
