using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Notes.Database;
using Notes.InputModels;
using Notes.ViewModels;
using Notes.Models;
using Notes.Mappers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Threax.AspNetCore.Halcyon.Ext;

namespace Notes.Repository
{
    public partial class NoteRepository : INoteRepository
    {
        private AppDbContext dbContext;
        private AppMapper mapper;

        public NoteRepository(AppDbContext dbContext, AppMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<NoteCollection> List(NoteQuery query)
        {
            var dbQuery = await query.Create(this.Entities);

            var total = await dbQuery.CountAsync();
            dbQuery = dbQuery.Skip(query.SkipTo(total)).Take(query.Limit);
            var results = await dbQuery.ToListAsync();

            return new NoteCollection(query, total, results.Select(i => mapper.MapNote(i, new Note())));
        }

        public async Task<Note> Get(Guid noteId)
        {
            var entity = await this.Entity(noteId);
            return mapper.MapNote(entity, new Note());
        }

        public async Task<Note> Add(NoteInput note)
        {
            var entity = mapper.MapNote(note, new NoteEntity());
            this.dbContext.Add(entity);
            await SaveChanges();
            return mapper.MapNote(entity, new Note());
        }

        public async Task<Note> Update(Guid noteId, NoteInput note)
        {
            var entity = await this.Entity(noteId);
            if (entity != null)
            {
                mapper.MapNote(note, entity);
                await SaveChanges();
                return mapper.MapNote(entity, new Note());
            }
            throw new KeyNotFoundException($"Cannot find note {noteId.ToString()}");
        }

        public async Task Delete(Guid id)
        {
            var entity = await this.Entity(id);
            if (entity != null)
            {
                Entities.Remove(entity);
                await SaveChanges();
            }
        }

        public virtual async Task<bool> HasNotes()
        {
            return await Entities.CountAsync() > 0;
        }

        public virtual async Task AddRange(IEnumerable<NoteInput> notes)
        {
            var entities = notes.Select(i => mapper.MapNote(i, new NoteEntity()));
            this.dbContext.Notes.AddRange(entities);
            await SaveChanges();
        }

        protected virtual async Task SaveChanges()
        {
            await this.dbContext.SaveChangesAsync();
        }

        private DbSet<NoteEntity> Entities
        {
            get
            {
                return dbContext.Notes;
            }
        }

        private Task<NoteEntity> Entity(Guid noteId)
        {
            return Entities.Where(i => i.NoteId == noteId).FirstOrDefaultAsync();
        }
    }
}