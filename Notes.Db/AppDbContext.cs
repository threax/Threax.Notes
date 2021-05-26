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
        public static string SchemaName { get; set; } = "dbo"; //Keep this here, it is needed during ef tools runs
                                                               //After creating a migration replace "dbo" with AppDbContext.SchemaName

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<NoteEntity> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema(AppDbContext.SchemaName);
        }
    }
}
