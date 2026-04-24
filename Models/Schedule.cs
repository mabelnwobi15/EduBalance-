using Microsoft.AspNetCore.Identity;

namespace EduBalance.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; }

        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        public string Day { get; set; }
        public string Subject { get; set; }
        public TimeSpan Time { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
