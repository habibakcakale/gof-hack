﻿using Hack.Data;
using Hack.Domain;
using Hack.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nensure;
using Swashbuckle.AspNetCore.Swagger;

namespace Hack.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Hack.Web", Version = "v1" });
            });
            services.AddScoped<IContextFactory, HackContextFactory>();
            RegisterConfigurations(Configuration, services);
            RegisterRepositories(services);
            RegisterServices(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hack.Web v1");
            });

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseMvcWithDefaultRoute();
        }

        private void RegisterConfigurations(IConfiguration config, IServiceCollection services)
        {
            Ensure.NotNull(config, services);

            var dbConfig = Configuration.GetSection("DbConfig").Get<DbConfig>();
            services.AddSingleton(dbConfig);

            var authConfig = Configuration.GetSection("Authentication").Get<AuthenticationConfig>();
            HackConfig.Instance = new HackConfig(authConfig);
            services.AddSingleton(authConfig.Token);
            services.AddSingleton(authConfig.Credentials);
        }

        private void RegisterRepositories(IServiceCollection services)
        {
            Ensure.NotNull(services);
            services.AddScoped<IUserRepo, UserRepo>();
        }

        private void RegisterServices(IServiceCollection services)
        {
            Ensure.NotNull(services);
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IJiraService, JiraService>();
        }
    }
}