using Microsoft.AspNetCore.Identity;

namespace EduBalance.Models
{
    public class StressCheckIn
    {
        public int StressCheckInId { get; set; }

        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        public int StressLevel { get; set; }
        public string Cause { get; set; }
        public DateTime DateLogged { get; set; } = DateTime.Now;

    }
}
