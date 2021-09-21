using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using QuizTopics.Candidate.Persistence;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]

namespace QuizTopics.Candidate.API
{
    public class Startup
    {
        private const string AllowSpecificOrigins = "AllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPersistence(this.Configuration.GetConnectionString("DefaultConnection"));

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                })
                .AddGoogle(options => //TODO: Az Key-Vault or Secrets
                {
                    options.ClientId = "844964698772-jn8f8fp3k1fq7ic8gb9dvr8khfpmto7l.apps.googleusercontent.com";
                    options.ClientSecret = "JYUo4p6Y1_BjhJhsHzC4JVtP";
                });

            //TODO: Fix: not working with google redirect
            services.AddCors(options =>
            {
                options.AddPolicy(AllowSpecificOrigins, // TODO: be more restrictive
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "QuizTopics.Candidate.API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Google Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            Scopes = new Dictionary<string, string>
                            {
                                { "openid", "https://www.googleapis.com/auth/drive.metadata.readonly" } // TODO: correct scopes?
                            },
                            AuthorizationUrl = new Uri("https://accounts.google.com/o/oauth2/v2/auth")
                        }
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "QuizTopics.Candidate.API v1");
                    c.OAuthClientId("844964698772-jn8f8fp3k1fq7ic8gb9dvr8khfpmto7l.apps.googleusercontent.com");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(AllowSpecificOrigins);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}