using Microsoft.AspNetCore.Identity;

namespace EduBalance.Models
{
    public class MoodLog
    {
        public int MoodLogId { get; set; }

        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        public string MoodType { get; set; }
        public string Notes { get; set; }
        public DateTime DateLogged { get; set; } = DateTime.Now;
    }
}
