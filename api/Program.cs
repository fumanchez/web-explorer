using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebExplorer.Api {
    public class Program {
        public static void Main(string[] args) {
            var hostBuilder = Host.CreateDefaultBuilder(args);

            hostBuilder.ConfigureWebHostDefaults(builder => {
                builder.UseStartup<Startup>();
            });

            hostBuilder.Build().Run();
        }
    }
}
