using System;
using System.Collections.Generic;
using System.Reflection;
using IQuality.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Raven.Client.Documents;
using Raven.Client.Documents.Conventions;
using Raven.DependencyInjection;
using Raven.Identity;

namespace IQuality.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

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
            
            // Setup RavenDB session and authorization
            services
                .AddSingleton(documentStore)
                .AddSingleton(s => s.GetService<IDocumentStore>().Changes())
                .AddRavenDbAsyncSession()
                .AddIdentity<ApplicationUser, IdentityRole>()
                .AddRavenDbIdentityStores<ApplicationUser>();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
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
