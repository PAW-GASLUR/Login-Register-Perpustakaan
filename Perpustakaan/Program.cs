using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Perpustakaan
{
    /// <summary>
    /// Main class
    /// </summary>
    /// <remarks>
    /// class program
    /// </remarks>
    public class Program
    {
        /// <summary>
        /// Class Program
        /// </summary>
        /// <param name="args">Parameter</param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Function createwebhostbuilder
        /// </summary>
        /// <param name="args"></param>
        /// <returns>createwebhosbuilder</returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
