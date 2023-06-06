using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ToDo.App.Infrastructure.Persistence;

namespace ToDo.App.Infrastructure.Extensions
{
    public static class SeedDataExtension
    {
        public static async Task InitializeData(this IApplicationBuilder applicationBuilder, IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var seedInterface = scope.ServiceProvider.GetRequiredService<ISeedData>();

            await seedInterface.Seed();
            scope.Dispose();
        }
    }
}
