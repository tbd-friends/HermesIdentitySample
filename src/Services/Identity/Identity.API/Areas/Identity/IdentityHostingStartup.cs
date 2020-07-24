﻿using Identity.API.Areas.Identity;
using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]
namespace Identity.API.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}