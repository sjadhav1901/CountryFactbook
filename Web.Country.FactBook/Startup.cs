using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Db.Core.Utilites;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Web.Country.FactBook.ApiIntegrations;
using Web.Country.FactBook.Helpers;
using Web.Country.FactBook.Repositories;

namespace Web.Country.FactBook
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            this.Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IDataSettings, DataSettings>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICountryRepository, CountryRepository>();
            services.AddTransient<ICurrencyRepository, CurrencyRepository>();
            services.AddTransient<ILanguageRepository, LanguageRepository>();
            services.AddTransient<IFeatureRepository, FeatureRepository>();
            services.AddTransient<IRecentActivityRepository, RecentActivityRepository>();
            services.AddTransient<IRoleFeatureMappingRepository, RoleFeatureMappingRepository>();
            services.AddTransient<ICountryLanguageMappingRepository, CountryLanguageMappingRepository>();
            services.AddTransient<ICountryCurrencyMappingRepository, CountryCurrencyMappingRepository>();
            services.AddTransient<IApiCountryAll, ApiCountryAll>();
            services.AddTransient<IPasswordHasher<string>, PasswordHasher<string>>();
            services.AddTransient<IActivityHelper, ActivityHelper>();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Authentication}/{action=SignIn}/{id?}");
            });
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "dashBoard",
                    template: "{controller=DashBoard}/{action=DashBoard}/{id?}");
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "Contents")),
                RequestPath = "/contents"
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), "Scripts")),
                RequestPath = "/scripts"
            });

            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<Web.Core.Profiles.AutoMapperProfile>();
            });
        }
    }
}
