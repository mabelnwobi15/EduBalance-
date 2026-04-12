using EduBalance.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EduBalance.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
     
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<MoodLog> MoodLogs { get; set; }
        public DbSet<StressCheckIn> StressCheckIns { get; set; }
        public DbSet<StudyBalance> StudyBalances { get; set; }

    }
}
