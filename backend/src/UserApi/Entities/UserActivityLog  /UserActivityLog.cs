// UserApi.Entities.UserActivityLog.UserActivityLog.cs

using System;

namespace UserApi.Entities.UserActivityLog
{
    public class UserActivityLog
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime ActivityTime { get; set; }
        public string ActivityType { get; set; }
        // Add other properties as needed for logging user activity
    }
}
