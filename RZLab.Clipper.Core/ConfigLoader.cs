using Microsoft.Extensions.Configuration;
using System.IO;

namespace RZLab.Clipper.Core;
public static class ConfigLoader
{
    public static IConfiguration Load()
    {
        return new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
    }
}
