using Microsoft.EntityFrameworkCore;
 
namespace BeltExam.Models
{
    public class UserContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }

        public DbSet<Users> users { get; set;}
        public DbSet<Participants> participants { get; set;}
        public DbSet<Activities> activities { get; set; }
    }
}