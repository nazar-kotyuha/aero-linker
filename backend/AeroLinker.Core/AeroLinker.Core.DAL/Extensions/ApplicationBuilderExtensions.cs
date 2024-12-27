using AeroLinker.Core.DAL.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AeroLinker.Core.DAL.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void UseAeroLinkerCoreContext(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();
        using var context = scope?.ServiceProvider.GetRequiredService<AeroLinkerCoreContext>();
        context?.Database.Migrate();
    }
}