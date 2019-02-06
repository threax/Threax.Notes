using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Notes.Repository;
using Threax.AspNetCore.Halcyon.Ext;
using Notes.ViewModels;
using Notes.InputModels;
using Notes.Models;
using Microsoft.AspNetCore.Authorization;

namespace Notes.Controllers.Api
{
    [Route("api/[controller]")]
    [ResponseCache(NoStore = true)]
    [Authorize(AuthenticationSchemes = AuthCoreSchemes.Bearer, Roles = Roles.EditNotes)]
    public partial class NotesController : Controller
    {
        private INoteRepository repo;

        public NotesController(INoteRepository repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [HalRel(CrudRels.List)]
        public async Task<NoteCollection> List([FromQuery] NoteQuery query)
        {
            return await repo.List(query);
        }

        [HttpGet("{NoteId}")]
        [HalRel(CrudRels.Get)]
        public async Task<Note> Get(Guid noteId)
        {
            return await repo.Get(noteId);
        }

        [HttpPost]
        [HalRel(CrudRels.Add)]
        [AutoValidate("Cannot add new note")]
        public async Task<Note> Add([FromBody]NoteInput note)
        {
            return await repo.Add(note);
        }

        [HttpPut("{NoteId}")]
        [HalRel(CrudRels.Update)]
        [AutoValidate("Cannot update note")]
        public async Task<Note> Update(Guid noteId, [FromBody]NoteInput note)
        {
            return await repo.Update(noteId, note);
        }

        [HttpDelete("{NoteId}")]
        [HalRel(CrudRels.Delete)]
        public async Task Delete(Guid noteId)
        {
            await repo.Delete(noteId);
        }
    }
}