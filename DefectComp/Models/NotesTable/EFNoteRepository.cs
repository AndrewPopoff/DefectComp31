using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DefectComp.Models.NotesTable
{
    public class EFNoteRepository : INoteRepository
    {
        private ApplicationDBContext context;
        public ApplicationDBContext Context => context;

        public EFNoteRepository(ApplicationDBContext ctx) => context = ctx;

        public IEnumerable<Note> Notes => context.Notes;

        // удалить запись
        public Note Delete(int noteID)
        {
            Note dbEntry = context.Notes.FirstOrDefault(p => p.NoteID == noteID);
            if (dbEntry != null)
            {
                context.Notes.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        // сохранить запись
        public void Save(Note note)
        {
            if (note.NoteID == 0)
            {
                context.Notes.Add(note);
            }
            else
            {
                Note dbEntry = context.Notes.FirstOrDefault(p => p.NoteID == note.NoteID);
                if (dbEntry != null)
                {
                    dbEntry.NoteID = note.NoteID;
                    dbEntry.NoteText = note.NoteText;
                    dbEntry.UserID = note.UserID;
                    dbEntry.NoteDate = note.NoteDate;
                    dbEntry.DataID = note.DataID;
                }
            }
            context.SaveChanges();
        }
    }
}







