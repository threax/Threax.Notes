using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Halcyon.HAL.Attributes;
using Threax.AspNetCore.Halcyon.Ext;
using Threax.AspNetCore.Models;
using Notes.Models;
using Threax.AspNetCore.Halcyon.Ext.ValueProviders;

namespace Notes.InputModels 
{
    [HalModel]
    public partial class NoteInput : INote
    {
        [TextAreaUiType()]
        public String Text { get; set; }

    }
}
