
using Microsoft.EntityFrameworkCore;

namespace DemoUsersManagementCommandSide.Infrastructuer.Persistence.DbInitializer
{
    public class DbInitializer(ApplicationDbContext context) : IDbInitializer
    {
        private readonly ApplicationDbContext _context = context;
        public async Task InitializeAsync()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Any())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception)
            { }
        }
    }
    
    
}

