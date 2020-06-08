using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
using IQuality.Api.Hubs;
using IQuality.Models.Authentication;
using IQuality.Models.Chat;
using IQuality.Models.Chat.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using IdentityRole = Raven.Identity.IdentityRole;
using IQuality.DomainServices;

namespace IQuality.Api
{
    public class Startup
    {
        private readonly IDocumentStore DocumentStore;

        public Startup(IConfiguration configuration, IHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;

            DocumentStore = new DocumentStore
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
                    },
                    FindCollectionName = type =>
                    {
                        if (typeof(BaseMessage).IsAssignableFrom(type))
                            return "Message";

                        if (typeof(BaseChat).IsAssignableFrom(type))
                            return "Chat";

                        return DocumentConventions.DefaultGetCollectionName(type);
                    }
                }
            }.Initialize();
        }

        private IConfiguration Configuration { get; }
        private IHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers().AddNewtonsoftJson(o => { o.SerializerSettings.CheckAdditionalContent = true; });
            services.AddSignalR();

            services.AddDependencies(Environment);

            // Setup RavenDB session and authorization
            services
                .AddSingleton(DocumentStore)
                .AddSingleton(s => s.GetService<IDocumentStore>().Changes())
                .AddRavenDbAsyncSession()
                .AddIdentity<ApplicationUser, IdentityRole>()
                .AddRavenDbIdentityStores<ApplicationUser>();

            var key = Encoding.ASCII.GetBytes(Configuration["Jwt:AudienceSecret"]);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                    x.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            var path = context.HttpContext.Request.Path;

                            if (string.IsNullOrWhiteSpace(accessToken)) return Task.CompletedTask;

                            // SignalR chat hub correct token
                            if (path.StartsWithSegments("/hub"))
                                context.Token = accessToken;

                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = context =>
                        {
                            context.HttpContext.Items.Add("token_error", true);
                            return Task.CompletedTask;
                        },
                        OnChallenge = context =>
                        {
                            context.HandleResponse();
                            if (context.HttpContext.Items.All(t => t.Key.ToString() != "token_error"))
                                return Task.CompletedTask;

                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            context.Response.WriteAsync(JsonConvert.SerializeObject(new
                                { key = "UnknownToken", message = "Invalid token provided." })).Wait();
                            return Task.CompletedTask;
                        }
                    };
                });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            IdentityDataInitializer.SeedRoles(roleManager);
            IdentityDataInitializer.SeedUsers(Configuration, userManager);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var notificationsTimer = new NotificationsTimer(DocumentStore);
            notificationsTimer.StartTimer(int.Parse(Configuration["Notification:Interval"]));

            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .WithOrigins("http://localhost:4200")
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/hub");
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

    public static class IdentityDataInitializer
    {
        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in Roles.RoleArray)
            {
                if (!roleManager.RoleExistsAsync(role).Result)
                    roleManager.CreateAsync(new IdentityRole(role)).Wait();
            }
        }

        public static void SeedUsers(IConfiguration config, UserManager<ApplicationUser> userManager)
        {
            var user = new ApplicationUser
            {
                UserName = config["DefaultAccount:Email"],
                Email = config["DefaultAccount:Email"],
                EmailConfirmed = true,
                FirstTime = true,
                Address = new Address
                {
                    City = config["DefaultAccount:Address:City"],
                    Country = config["DefaultAccount:Address:Country"],
                    HouseNumber = int.Parse(config["DefaultAccount:Address:HouseNumber"]),
                    StreetName = config["DefaultAccount:Address:StreetName"],
                    ZipCode = config["DefaultAccount:Address:ZipCode"]
                },
                Name = new FullName
                {
                    First = config["DefaultAccount:Name:First"],
                    Last = config["DefaultAccount:Name:Last"]
                }
            };

            // If the default account exists, we don't have to create it again!
            if (userManager.FindByEmailAsync(user.Email).Result != null) return;

            IdentityResult result = userManager.CreateAsync(user, config["DefaultAccount:Password"]).Result;

            if (result.Succeeded)
                userManager.AddToRoleAsync(user, Roles.Admin);
        }
    }
}