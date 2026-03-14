using Notes.Database;
using Notes.InputModels;
using Notes.ViewModels;
using System;
using System.Linq;

namespace Notes.Mappers
{
    public partial class AppMapper
    {
        public NoteEntity MapNote(NoteInput src, NoteEntity dest)
        {
            dest.FirstLine = ExtractFirstLine(src.Text);
            dest.Text = src.Text;
            dest.Created = GetCreated(dest.Created);
            dest.Modified = DateTime.UtcNow;
            
            return dest;
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
            dest.NoteId = src.NoteId;
            dest.Text = src.Text;
            dest.Created = src.Created;
            dest.Modified = src.Modified;

            return dest;
        }

        private string ExtractFirstLine(string text)
        {
            if (text != null)
            {
                var firstLineIndex = text.IndexOf('\n');
                if (firstLineIndex != -1)
                {
                    return text.Substring(0, firstLineIndex);
                }
            }
            return text;
        }
    }
}