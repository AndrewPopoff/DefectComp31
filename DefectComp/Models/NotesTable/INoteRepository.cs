using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefectComp.Models.NotesTable
{
    public interface INoteRepository
    {
        IEnumerable<Note> Notes { get; }
        void Save(Note note);
        Note Delete(int noteID);
    }
}
