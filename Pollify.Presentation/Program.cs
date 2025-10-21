
using Microsoft.Extensions.Hosting;
using ServiceRegistry;

Console.WriteLine("dd");

var host = CreateHostBuilder().Build();
var serviceProvider = host.Services;



static IHostBuilder CreateHostBuilder()
{
    return Host.CreateDefaultBuilder().ConfigureServices(services =>
        services.Initialize());
}
