using Halcyon.HAL.Attributes;
using Notes.Controllers;
using Notes.Models;
using Notes.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Threax.AspNetCore.Halcyon.Ext;
using Threax.AspNetCore.Halcyon.Ext.ValueProviders;
using Threax.AspNetCore.Models;
using System.ComponentModel.DataAnnotations;

namespace Notes.InputModels
{
    [HalModel]
    [CacheEndpointDoc]
    public partial class NoteQuery : PagedCollectionQuery, INoteQuery
    {
        /// <summary>
        /// Lookup a note by id.
        /// </summary>
        public Guid? NoteId { get; set; }

        /// <summary>
        /// Populate an IQueryable. Does not apply the skip or limit.
        /// </summary>
        /// <param name="query">The query to populate.</param>
        /// <returns>The query passed in populated with additional conditions.</returns>
        public Task<IQueryable<NoteEntity>> Create(IQueryable<NoteEntity> query)
        {
            if (NoteId != null)
            {
                query = query.Where(i => i.NoteId == NoteId);
            }
            else
            {
                //Customize query further
                query = query.OrderByDescending(i => i.Modified);
            }

            return Task.FromResult(query);
        }
    }
}