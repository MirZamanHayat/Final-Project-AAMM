// UserApi.Entities.UserActivityLog.UserActivityLogContext.cs

using Microsoft.EntityFrameworkCore;

namespace UserApi.Entities.UserActivityLog
{
    public class UserActivityLogContext : DbContext
    {
        public UserActivityLogContext(DbContextOptions<UserActivityLogContext> options) : base(options)
        {
        }

        public DbSet<UserActivityLog> UserActivityLogs { get; set; }

        // Add any additional configurations or methods as needed
    }
}
