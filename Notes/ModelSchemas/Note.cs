using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Threax.AspNetCore.Models;

namespace Notes.ModelSchemas
{
    [RequireAuthorization(typeof(Roles), nameof(Roles.EditNotes))]
    public abstract class Note
    {
        [TextAreaUiType]
        public String Text { get; set; }
    }
}
