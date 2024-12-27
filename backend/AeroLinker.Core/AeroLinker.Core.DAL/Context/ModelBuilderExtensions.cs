using Microsoft.EntityFrameworkCore;
using AeroLinker.Core.DAL.Context.EntityConfigurations;

namespace AeroLinker.Core.DAL.Context;

public static class ModelBuilderExtensions
{
    public static void Configure(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfig).Assembly);
    }
}