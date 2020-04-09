using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using IQuality.Api.Extensions;
using IQuality.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Raven.Client.Documents;
using Raven.Client.Documents.Conventions;
using Raven.DependencyInjection;
using Raven.Identity;
using IQuality.Api.Controllers;
using IQuality.Infrastructure.Database.Index;
using Raven.Client.Documents.Indexes;

namespace IQuality.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        private IConfiguration Configuration { get; }
        private IHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSignalR();

            var documentStore = new DocumentStore
            {
                Urls = new[]
                {
                    Configuration["Raven:Url"]
                },
                Database = Configuration["Raven:Name"],
                Conventions = new DocumentConventions
                {
                    IdentityPartsSeparator = "-",
                    JsonContractResolver = new IncludeNonPublicMembersContractResolver(),
                    CustomizeJsonSerializer = serializer =>
                    {
                        serializer.ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor;
                        serializer.CheckAdditionalContent = true;
                    }
                }
            }.Initialize();
            
            services.AddDependencies(Environment);
            
            // Add index to RavenDB
            IndexCreation.CreateIndexes(typeof(ChatIndex).Assembly, documentStore);
            
            // Setup RavenDB session and authorization
            services
                .AddSingleton(documentStore)
                .AddSingleton(s => s.GetService<IDocumentStore>().Changes())
                .AddRavenDbAsyncSession()
                .AddIdentity<ApplicationUser, IdentityRole>()
                .AddRavenDbIdentityStores<ApplicationUser>();

            services
                .AddAuthentication(o =>
                {
                    o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = true;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:AudienceId"],
                        IssuerSigningKey =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:AudienceSecret"]))
                    };
                });
            //
            //     services.AddAuthentication(options =>
            //     {
            //         options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //         options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //     }).AddJwtBearer(o =>
            //     {
            //         o.TokenValidationParameters = new TokenValidationParameters
            //         {
            //             ValidateIssuer = true,
            //             ValidateAudience = true,
            //             ValidateLifetime = true,
            //             ValidateIssuerSigningKey = true,
            //             ValidIssuer = Configuration["Jwt:Issuer"],
            //             ValidAudience = Configuration["Jwt:AudienceId"],
            //             IssuerSigningKey =
            //                 new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:AudienceSecret"]))
            //         };
            //
            //         o.Events = new JwtBearerEvents
            //         {
            //             OnMessageReceived = context =>
            //             {
            //                 var accessToken = context.Request.Query["access_token"];
            //
            //                 var path = context.HttpContext.Request.Path;
            //
            //                 if (string.IsNullOrWhiteSpace(accessToken)) return Task.CompletedTask;
            //
            //                 // SignalR chat hub correct token
            //                 if (path.StartsWithSegments("/chatHub"))
            //                     context.Token = accessToken;
            //
            //                 return Task.CompletedTask;
            //             },
            //             OnAuthenticationFailed = context =>
            //             {
            //                 context.HttpContext.Items.Add("token_error", true);
            //                 return Task.CompletedTask;
            //             },
            //             OnChallenge = context =>
            //             {
            //                 context.HandleResponse();
            //                 if (context.HttpContext.Items.All(t => t.Key.ToString() != "token_error"))
            //                     return Task.CompletedTask;
            //
            //                 context.Response.StatusCode = 401;
            //                 context.Response.ContentType = "application/json";
            //                 context.Response.WriteAsync(JsonConvert.SerializeObject(new
            //                     { key = "UnknownToken", message = "Invalid token provided." })).Wait();
            //                 return Task.CompletedTask;
            //
            //             }
            //         };
            //     });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseRouting();

            app.UseCors(c =>
                c.AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin()
            );

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHubSocket>("/chatHub");
            });
        }
    }

    public class IncludeNonPublicMembersContractResolver : DefaultContractResolver
    {
        protected override List<MemberInfo> GetSerializableMembers(Type type)
        {
            var members = new List<MemberInfo>();

            members.AddRange(type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance));
            members.AddRange(type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance));

            return members;
        }
    }
}