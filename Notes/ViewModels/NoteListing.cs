using Halcyon.HAL.Attributes;
using Notes.Controllers.Api;
using Notes.Models;
using System;
using Threax.AspNetCore.Halcyon.Ext;
using Threax.AspNetCore.Models;

namespace Notes.ViewModels 
{
    [HalModel]
    [CacheEndpointDoc]
    [HalSelfActionLink(typeof(NotesController), nameof(NotesController.Get))]
    [HalActionLink(typeof(NotesController), nameof(NotesController.Update))]
    [HalActionLink(typeof(NotesController), nameof(NotesController.Delete))]
    public partial class NoteListing : INoteId
    {
        public Guid NoteId { get; set; }

        [TextAreaUiType()]
        public String FirstLine { get; set; }

        [UiOrder(0, 2147483646)]
        public DateTime Created { get; set; }

        [UiOrder(0, 2147483647)]
        public DateTime Modified { get; set; }

    }
}
