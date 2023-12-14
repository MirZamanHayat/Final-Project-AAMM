// UserApi.Repositories.UserActivityLogRepository.cs

using System.Threading.Tasks;

namespace UserApi.Repositories
{
    public class UserActivityLogRepository : IUserActivityLogRepository
    {
        private readonly UserActivityLogContext _context;

        public UserActivityLogRepository(UserActivityLogContext context)
        {
            _context = context;
        }

        // Implement methods for interacting with user activity logs (e.g., AddLog, GetLogs, etc.)
    }
}
