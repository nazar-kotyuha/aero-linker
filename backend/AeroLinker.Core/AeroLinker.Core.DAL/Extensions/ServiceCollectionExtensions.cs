using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AeroLinker.Core.DAL.Context;

namespace AeroLinker.Core.DAL.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddAeroLinkerCoreContext(this IServiceCollection services, IConfiguration configuration)
    {
        var aerolinkerCoreDbConnectionString = "AeroLinkerCoreDBConnection";
        var connectionsString = configuration.GetConnectionString(aerolinkerCoreDbConnectionString);
        services.AddDbContext<AeroLinkerCoreContext>(options =>
            options.UseSqlServer(
                connectionsString,
                opt => opt.MigrationsAssembly(typeof(AeroLinkerCoreContext).Assembly.GetName().Name)));
    }
}
