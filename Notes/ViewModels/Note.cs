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
using Notes.Controllers.Api;
using Threax.AspNetCore.Halcyon.Ext.ValueProviders;

namespace Notes.ViewModels 
{
    [HalModel]
    [CacheEndpointDoc]
    [HalSelfActionLink(typeof(NotesController), nameof(NotesController.Get))]
    [HalActionLink(typeof(NotesController), nameof(NotesController.Update))]
    [HalActionLink(typeof(NotesController), nameof(NotesController.Delete))]
    public partial class Note : INote, INoteId, ICreatedModified
    {
        public Guid NoteId { get; set; }

        [TextAreaUiType()]
        public String Text { get; set; }

        [UiOrder(0, 2147483646)]
        public DateTime Created { get; set; }

        [UiOrder(0, 2147483647)]
        public DateTime Modified { get; set; }

    }
}
