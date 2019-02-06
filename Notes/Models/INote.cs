using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Halcyon.HAL.Attributes;
using Threax.AspNetCore.Halcyon.Ext;
using Threax.AspNetCore.Models;

namespace Notes.Models 
{
    public partial interface INote 
    {
        String Text { get; set; }

    }

    public partial interface INoteId
    {
        Guid NoteId { get; set; }
    }    

    public partial interface INoteQuery
    {
        Guid? NoteId { get; set; }

    }
}