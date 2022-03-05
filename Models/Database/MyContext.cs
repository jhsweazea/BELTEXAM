using Microsoft.EntityFrameworkCore;

namespace BELTEXAM.Models
{
    public class MyContext :DbContext
    {
        public MyContext (DbContextOptions options) : base(options){}
        
        public DbSet<User> Users {get;set;}
        public DbSet<Activity> Activities {get;set;}
        public DbSet<RSVP> RSVPs {get;set;}
    }
}