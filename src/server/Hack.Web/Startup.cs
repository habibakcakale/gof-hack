using FluentValidation.AspNetCore;
using Hack.Data;
using Hack.Domain;
using Hack.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Nensure;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Text;

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
            services.AddCors(config => config.AddDefaultPolicy(new CorsPolicyBuilder().AllowAnyOrigin().AllowAnyMethod().AllowCredentials().AllowAnyHeader().Build()));
            AddMvcWithExceptionHandling(services);
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info { Title = "Hack.Web", Version = "v1" }); });
            RegisterConfigurations(Configuration, services);
            AddAuthentication(services);
            RegisterContextAndRepositories(services);
            RegisterServices(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hack.Web v1"));
            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseAuthentication();
            app.UseMvc();
            app.UseMvcWithDefaultRoute();
            RunStartupTasks(app.ApplicationServices);
        }

        private void RunStartupTasks(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var jiraCredentialsValid = scope.ServiceProvider.GetService<IJiraService>().ValidateCredentials();
                jiraCredentialsValid.Wait();
                if (!jiraCredentialsValid.Result)
                {
                    throw new InvalidOperationException("Jira credentials are not valid. Stopping application.");
                }
            }
        }

        private void AddAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(config =>
            {
                config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(config =>
            {
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
                var token = HackConfig.Instance.Auth.Token;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Secret)),

                    ValidateIssuer = true,
                    ValidIssuer = token.Issuer,

                    ValidateAudience = true,
                    ValidAudience = token.Audience
                };
            });
        }

        private void AddMvcWithExceptionHandling(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add<ValidateModelStateAttribute>();
            })
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<GetTasksRequestValidator>())
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddTransient<ExceptionHandlingMiddleware>();
        }

        private void RegisterConfigurations(IConfiguration config, IServiceCollection services)
        {
            Ensure.NotNull(config, services);

            var db = Configuration.GetSection("DbConfig").Get<DbConfig>();
            services.AddSingleton(db);
            var auth = Configuration.GetSection("Authentication").Get<AuthenticationConfig>();
            var jiraCredentials = Configuration.GetSection("JiraCredentials").Get<JiraCredentialsConfig>();
            HackConfig.Instance = new HackConfig(auth, jiraCredentials);
            services.AddSingleton(auth.Token);
            services.AddSingleton(auth.Credentials);
            services.AddSingleton(jiraCredentials);
        }

        private void RegisterContextAndRepositories(IServiceCollection services)
        {
            Ensure.NotNull(services);
            services.AddScoped<IContextFactory, HackContextFactory>();
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