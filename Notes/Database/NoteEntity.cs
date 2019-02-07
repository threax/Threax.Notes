using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Halcyon.HAL.Attributes;
using Threax.AspNetCore.Halcyon.Ext;
using Threax.AspNetCore.Models;
using Threax.AspNetCore.Tracking;
using Notes.Models;

namespace Notes.Database 
{
    public partial class NoteEntity : INote, INoteId, ICreatedModified
    {
        [Key]
        public Guid NoteId { get; set; }

        public String Text { get; set; }

        public String FirstLine { get; set; }

        public DateTime Created { get; set; }

        public DateTime Modified { get; set; }

    }
}
