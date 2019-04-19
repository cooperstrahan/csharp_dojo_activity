using Microsoft.EntityFrameworkCore;
 
namespace BeltExam.Models
{
    public class UserContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public UserContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users {get; set;}
        public DbSet<Event> Events { get; set; }
        public DbSet<Participant> Participants { get; set; }
    }
}
