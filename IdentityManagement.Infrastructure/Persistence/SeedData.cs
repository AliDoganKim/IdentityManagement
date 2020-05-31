using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IdentityManagement.Infrastructure.Persistence
{
    public class SeedData
    {
        public static void EnsureSeedData(IServiceProvider provider)
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            provider.GetRequiredService<AppIdentityDbContext>().Database.Migrate();
            provider.GetRequiredService<AppPersistedGrantDbContext>().Database.Migrate();
            provider.GetRequiredService<AppConfigurationDbContext>().Database.Migrate();

            var context = provider.GetRequiredService<AppConfigurationDbContext>();
            if (!context.Clients.Any())
            {
                var clients = new List<Client>();
                configuration.GetSection("IdentityServer:Clients").Bind(clients);
                foreach (var client in clients)
                    context.Clients.Add(client.ToEntity());
                context.SaveChanges();
            }
            if (!context.ApiResources.Any())
            {
                var apiResources = new List<ApiResource>();
                configuration.GetSection("IdentityServer:ApiResources").Bind(apiResources);
                foreach (var apiResource in apiResources)
                    context.ApiResources.Add(apiResource.ToEntity());
                context.SaveChanges();
            }
            if (!context.IdentityResources.Any())
            {
                var identityResources = new List<IdentityResource>();
                configuration.GetSection("IdentityServer:IdentityResources").Bind(identityResources);
                foreach (var identityResource in identityResources)
                    context.IdentityResources.Add(identityResource.ToEntity());
                context.SaveChanges();
            }
        }
    }
}