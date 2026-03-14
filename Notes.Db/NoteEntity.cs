using System;
using System.ComponentModel.DataAnnotations;

namespace Notes.Database
{
    public partial class NoteEntity
    {
        [Key]
        public Guid NoteId { get; set; }

        public String Text { get; set; }

        public String FirstLine { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

    }
}
