using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Identity.API.Configuration;
using Identity.API.Models;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Identity.API.Data
{
    public class ConfigurationDbSeeder
    {
        public async Task SeedAsync(ConfigurationDbContext context, IConfiguration configuration)
        {
            var clientUrls = new Dictionary<string, string>();

            clientUrls.Add("OrderingApi", configuration.GetValue<string>("OrderingApiClient"));
            clientUrls.Add("Dashboard", configuration.GetValue<string>("DashboardClient"));

            if (!context.Clients.Any())
            {
                await context.Clients.AddRangeAsync(Config.GetClients(clientUrls).Select(c => c.ToEntity()));
                await context.SaveChangesAsync();
            }

            if (!context.IdentityResources.Any())
            {
                await context.IdentityResources.AddRangeAsync(Config.GetResources().Select(c => c.ToEntity()));
                await context.SaveChangesAsync();
            }

            if (!context.ApiResources.Any())
            {
                await context.ApiResources.AddRangeAsync(Config.GetApis().Select(c => c.ToEntity()));
                await context.SaveChangesAsync();
            }
        }


        //private void SeedUsers()
        //{
        //    _logger.LogInformation("Seeding Users");
        //    var address = new
        //    {
        //        street_address = "One Hacker Way",
        //        locality = "Heidelberg",
        //        postal_code = 69118,
        //        country = "Germany"
        //    };

        //    var alice = new ApplicationUser()
        //    {
        //        Id = Guid.NewGuid().ToString(),
        //        UserName = "AliceSmith@email.com",
        //        Email = "AliceSmith@email.com",
        //        EmailConfirmed = true
        //    };
        //    _userManager.CreateAsync(alice, "P@assWord1").Wait();
        //    _userManager.AddClaimsAsync(alice, new List<Claim>
        //        {
        //            new Claim(JwtClaimTypes.Name, "Alice Smith"),
        //            new Claim(JwtClaimTypes.GivenName, "Alice"),
        //            new Claim(JwtClaimTypes.FamilyName, "Smith"),
        //            new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
        //            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
        //            new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
        //            new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address),
        //                IdentityServerConstants.ClaimValueTypes.Json)

        //        }).Wait();

        //    var bob = new ApplicationUser()
        //    {
        //        Id = Guid.NewGuid().ToString(),
        //        UserName = "BobSmith@email.com",
        //        Email = "BobSmith@email.com",
        //        EmailConfirmed = true,
        //    };
        //    _userManager.CreateAsync(bob, "P@assWord1").Wait();
        //    _userManager.AddClaimsAsync(bob, new List<Claim>
        //    {
        //        new Claim(JwtClaimTypes.Name, "Bob Smith"),
        //        new Claim(JwtClaimTypes.GivenName, "Bob"),
        //        new Claim(JwtClaimTypes.FamilyName, "Smith"),
        //        new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
        //        new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
        //        new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
        //        new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address),
        //            IdentityServerConstants.ClaimValueTypes.Json)
        //    }).Wait();
        //}
    }
}