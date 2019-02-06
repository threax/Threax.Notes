using Microsoft.EntityFrameworkCore;

namespace Notes.Database
{
    public partial class AppDbContext
    {
        public DbSet<NoteEntity> Notes { get; set; }
    }
}
