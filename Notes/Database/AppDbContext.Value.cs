using Microsoft.EntityFrameworkCore;

namespace Notes.Database
{
    public partial class AppDbContext
    {
        public DbSet<ValueEntity> Values { get; set; }
    }
}
