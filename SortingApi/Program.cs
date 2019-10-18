using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace SortingApi
{
    /// <summary>
    /// Main class that creates a WebHost and creates the Startup class
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) => WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
    }
}
