using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;


using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

using back_end.Repositorios;
using back_end.Controllers;
using back_end.Filtros;
using AutoMapper;
using back_end.Utilidades;

namespace back_end
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
            // Añadir Automapper, primera opcion comentada no funciona
            
            services.AddAutoMapper(typeof(AutoMapperProfiles));
            //var mapperConfig = new MapperConfiguration(mc => {
            //    mc.AddProfile(new AutoMapperProfiles());
            //});
            //IMapper mapper = mapperConfig.CreateMapper();
            //services.AddSingleton(mapper);
            ////


            services.AddDbContext<ApplicationDBContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
            services.AddCors(options => {
                var frontendURL = Configuration.GetValue<string>("frontend_url");
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(frontendURL).AllowAnyMethod().AllowAnyHeader()
                    .WithExposedHeaders(new string[] { "cantidadTotalRegistros" });
                });
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
            //services.AddResponseCaching();
            //services.AddTransient<IRepositorio, RepositorioEnMemoria>();
            ////services.AddScoped<IRepositorio, RepositorioEnMemoria>();
            //services.AddScoped<WeatherForecastController>();
            //services.AddTransient<FiltroDeAccion>();
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(FiltroDeExcepcion));
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            //ILogger<Startup> logger)
        {
            //app.Use(async (context, next) =>
            //{
            //    using (var swapStream = new MemoryStream())
            //    {
            //        var respuestaOriginal = context.Response.Body;
            //        context.Response.Body = swapStream;
            //        await next.Invoke();

            //        swapStream.Seek(0, SeekOrigin.Begin);
            //        string respuesta = new StreamReader(swapStream).ReadToEnd();
            //        swapStream.Seek(0, SeekOrigin.Begin);

            //        await swapStream.CopyToAsync(respuestaOriginal);
            //        context.Response.Body = respuestaOriginal;

            //        logger.LogInformation(respuesta);
            //    }
            // });
            //app.Map("/mapa1", (app) =>
            //{
            //    app.Run(async context =>
            //    {
            //        await context.Response.WriteAsync("Estoy interceptando un pipeline");
            //    });
            //});
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseResponseCaching();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
