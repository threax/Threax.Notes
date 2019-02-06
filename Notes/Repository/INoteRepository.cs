using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Notes.InputModels;
using Notes.ViewModels;
using Notes.Models;
using Threax.AspNetCore.Halcyon.Ext;

namespace Notes.Repository
{
    public partial interface INoteRepository
    {
        Task<Note> Add(NoteInput value);
        Task AddRange(IEnumerable<NoteInput> values);
        Task Delete(Guid id);
        Task<Note> Get(Guid noteId);
        Task<bool> HasNotes();
        Task<NoteCollection> List(NoteQuery query);
        Task<Note> Update(Guid noteId, NoteInput value);
    }
}