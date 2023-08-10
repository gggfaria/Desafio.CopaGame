using Desafio.CopaGame.Data.Jogos;
using Desafio.CopaGame.Domain.Consts;
using Desafio.CopaGame.Domain.Interfaces;
using Desafio.CopaGame.Filters;
using Desafio.CopaGame.Service.Interfaces;
using Desafio.CopaGame.Service.Jogos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using AutoMapper;
using Desafio.CopaGame.Service.AutoMapperConfigs;

namespace Desafio.CopaGame
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

            services.AddControllersWithViews(opt =>
            {
                opt.Filters.Add(typeof(CustomExceptionFilter));
            }).AddJsonOptions(o =>
            {
                  o.JsonSerializerOptions.IgnoreNullValues = true;
            });

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            ConfigRepositories(services);
            ConfigServices(services);
            RegisterHttpClient(services);
            ConfiAutoMapper(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }

        private void ConfigRepositories(IServiceCollection services)
        {
            services.AddScoped<IJogoRepository, JogoRepository>();
        }

        private void ConfigServices(IServiceCollection services)
        {
            services.AddScoped<IPartidaService, PartidaService>();
        }

        private void RegisterHttpClient(IServiceCollection service)
        {
            var configs = Configuration.GetSection("ApiUrl").Get<Dictionary<string, string>>();

            service.AddHttpClient();

            service.AddHttpClient(HttpClientConst.JOGO, c =>
            {
                c.BaseAddress = new Uri(configs["Url"]);
            });

        }
        private void ConfiAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(MapperConfigs)
            );
        }

    }
}
