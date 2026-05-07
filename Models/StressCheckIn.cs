using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace EduBalance.Models
{
    public class StressCheckIn

    {
        public int StressCheckInId { get; set; }

        public string? UserId { get; set; }
        public IdentityUser? User { get; set; }

        public int StressLevel { get; set; }

        [Required(ErrorMessage = "Please select what is causing your stress.")]
        public string Cause { get; set; }
        public DateTime DateLogged { get; set; } = DateTime.Now;

    }
}
