using System;
using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace Identity.API.Configuration
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApis()
        {
            var apis = new List<ApiResource>();

            apis.Add(new ApiResource("orders", "Orders Service"));
            apis.Add(new ApiResource("orders.signalrhub", "Orders SignalR Hub"));

            return apis;
        }

        public static IEnumerable<IdentityResource> GetResources()
        {
            var resources = new List<IdentityResource>();

            resources.Add(new IdentityResources.OpenId());
            resources.Add(new IdentityResources.Profile());

            return resources;
        }

        public static IEnumerable<Client> GetClients(Dictionary<string, string> clientsUrl)
        {
            var clients = new List<Client>();

            clients.Add(new Client
            {
                ClientId = "dashboard",
                ClientName = "Hermes Dashboard",
                ClientSecrets = new List<Secret>
                {
                    new Secret("secret".Sha256())
                },
                ClientUri = $"{clientsUrl["Dashboard"]}",
                AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                AllowAccessTokensViaBrowser = false,
                RequireConsent = false,
                AllowOfflineAccess = true,
                AlwaysIncludeUserClaimsInIdToken = true,
                RedirectUris = new List<string>
                {
                    $"{clientsUrl["Dashboard"]}/signin-oidc"
                },
                PostLogoutRedirectUris = new List<string>
                {
                    $"{clientsUrl["Dashboard"]}/signout-callback-oidc"
                },
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    "orders",
                    "orders.signalrhub"
                },
                AccessTokenLifetime = 60 * 60 * 2,
                IdentityTokenLifetime = 60 * 60 * 2,
            });

            clients.Add(new Client
            {
                ClientId = "orderingswaggerui",
                ClientName = "Ordering Swagger UI",
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowAccessTokensViaBrowser = true,

                RedirectUris = { $"{clientsUrl["OrderingApi"]}/swagger/oauth2-redirect.html" },
                PostLogoutRedirectUris = { $"{clientsUrl["OrderingApi"]}/swagger/" },

                AllowedScopes =
                {
                    "orders"
                }
            });

            return clients;
        }
    }
}