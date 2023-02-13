using Microsoft.EntityFrameworkCore;
using TestAPI.Data;

namespace TestAPI.Utils
{
    public static class DatabaseManagementService
    {
        // Getting the scope of our database context
        public static void MigrationInitialisation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                // Takes all of our migrations files and apply them against the database in case they are not implemented
                var services = serviceScope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<TestAPIContext>();
                    if (context.Database.GetPendingMigrations().Any())
                    {
                        context.Database.Migrate();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}
