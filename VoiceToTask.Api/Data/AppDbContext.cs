using Microsoft.EntityFrameworkCore;
using VoiceToTask.Api.Models;

namespace VoiceToTask.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ } 
        
        public DbSet<WorkTask> WorkTasks { get; set; }


    }
}
