using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace EduBalance.Models
{
    public class StudyBalance
    {
        [Key]
        public int StudyBalanceId { get; set; }  // ✅ FIXED NAME

        public string? UserId { get; set; }
        public IdentityUser? User { get; set; }

        public int StudyHours { get; set; }
        public int BreakHours { get; set; }

        public DateTime DateRecorded { get; set; } = DateTime.Now;
    }
}