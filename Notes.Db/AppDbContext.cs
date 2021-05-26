using Microsoft.EntityFrameworkCore;
using Threax.AspNetCore.UserBuilder.Entities;

namespace Notes.Database
{
    /// <summary>
    /// By default the app db context extends the UsersDbContext from Authorization. 
    /// This gives it the Users, Roles and UsersToRoles tables.
    /// </summary>
    public partial class AppDbContext : UsersDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<NoteEntity> Notes { get; set; }
    }
}
