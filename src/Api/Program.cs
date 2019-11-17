using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using API;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHostBuilder builder = WebHost.CreateDefaultBuilder(args);
            
            builder.ConfigureServices(collectionService => {
                collectionService.AddSingleton<IA>(new A());
            });
            
            // CreateWebHostBuilder(args).Build().Run();

            // builder.UseUrls(new []{ "http://localhost:9090", "http://192.168.43.159:8888" });
            builder.UseAwesomeServer(o => o.FolderPath = @"/Users/JamieAbner/Source/Api/Request");
            builder.UseStartup<Startup>().Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
