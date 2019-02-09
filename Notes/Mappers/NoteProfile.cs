using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Threax.AspNetCore.Models;
using Threax.AspNetCore.Tracking;
using Notes.InputModels;
using Notes.Database;
using Notes.ViewModels;
using System.Linq;

namespace Notes.Mappers
{
    public partial class AppMapper
    {
        public NoteEntity MapNote(NoteInput src, NoteEntity dest)
        {
            return mapper.Map(src, dest);
        }

        public IQueryable<NoteListing> MapNote(IQueryable<NoteEntity> src)
        {
            return src.Select(i => new NoteListing()
            {
                NoteId = i.NoteId,
                FirstLine = i.FirstLine,
                Created = i.Created,
                Modified = i.Modified,
            });
        }

        public Note MapNote(NoteEntity src, Note dest)
        {
            return mapper.Map(src, dest);
        }
    }

    public partial class NoteProfile : Profile
    {
        public NoteProfile()
        {
            //Map the input model to the entity
            MapInputToEntity(CreateMap<NoteInput, NoteEntity>()
                .ForMember(i => i.FirstLine, o => o.MapFrom((i, e) =>
                {
                    if (i.Text != null)
                    {
                        var firstLineIndex = i.Text.IndexOf('\n');
                        if (firstLineIndex != -1)
                        {
                            return i.Text.Substring(0, firstLineIndex);
                        }
                    }
                    return i.Text;
                }))
            );

            //Map the entity to the view model.
            MapEntityToView(CreateMap<NoteEntity, Note>());
        }

        void MapInputToEntity(IMappingExpression<NoteInput, NoteEntity> mapExpr)
        {
            mapExpr.ForMember(d => d.NoteId, opt => opt.Ignore())
                .ForMember(d => d.Created, opt => opt.MapFrom<ICreatedResolver>())
                .ForMember(d => d.Modified, opt => opt.MapFrom<IModifiedResolver>());
        }

        void MapEntityToView(IMappingExpression<NoteEntity, Note> mapExpr)
        {
            
        }
    }
}