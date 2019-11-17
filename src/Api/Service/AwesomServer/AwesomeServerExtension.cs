using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.DependencyInjection;

namespace API
{
    public static class AwesomeServerExtension
    {
        public static IWebHostBuilder UseAwesomeServer(this IWebHostBuilder hostBuilder, Action<AwesomeServerOptions> options)
        {
            return hostBuilder.ConfigureServices(services =>{
                services.Configure(options);
                services.AddSingleton<IServer, AwesomeServer>();
            });
        }

    }
}