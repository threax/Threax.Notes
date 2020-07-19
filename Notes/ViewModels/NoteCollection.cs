using Halcyon.HAL.Attributes;
using Notes.Controllers.Api;
using Notes.Models;
using Notes.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Threax.AspNetCore.Halcyon.Ext;

namespace Notes.ViewModels
{
    [HalModel]
    [CacheEndpointDoc]
    [HalSelfActionLink(typeof(NotesController), nameof(NotesController.List))]
    [HalActionLink(typeof(NotesController), nameof(NotesController.Get), DocsOnly = true)] //This provides access to docs for showing items
    [HalActionLink(typeof(NotesController), nameof(NotesController.List), DocsOnly = true)] //This provides docs for searching the list
    [HalActionLink(typeof(NotesController), nameof(NotesController.Update), DocsOnly = true)] //This provides access to docs for updating items if the ui has different modes
    [HalActionLink(typeof(NotesController), nameof(NotesController.Add))]
    [DeclareHalLink(typeof(NotesController), nameof(NotesController.List), PagedCollectionView<Object>.Rels.Next, ResponseOnly = true)]
    [DeclareHalLink(typeof(NotesController), nameof(NotesController.List), PagedCollectionView<Object>.Rels.Previous, ResponseOnly = true)]
    [DeclareHalLink(typeof(NotesController), nameof(NotesController.List), PagedCollectionView<Object>.Rels.First, ResponseOnly = true)]
    [DeclareHalLink(typeof(NotesController), nameof(NotesController.List), PagedCollectionView<Object>.Rels.Last, ResponseOnly = true)]
    public partial class NoteCollection : PagedCollectionViewWithQuery<NoteListing, NoteQuery>
    {
        public NoteCollection(NoteQuery query, int total, IEnumerable<NoteListing> items) : base(query, total, items)
        {
            
        }
    }
}