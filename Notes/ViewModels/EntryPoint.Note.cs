using Halcyon.HAL.Attributes;
using Threax.AspNetCore.Halcyon.Ext;
using Notes.Controllers.Api;

namespace Notes.ViewModels
{
    [HalActionLink(typeof(NotesController), nameof(NotesController.List), "ListNotes")]
    [HalActionLink(typeof(NotesController), nameof(NotesController.Add), "AddNote")]
    public partial class EntryPoint
    {
        
    }
}