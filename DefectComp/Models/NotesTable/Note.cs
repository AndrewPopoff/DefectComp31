using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DefectComp.Models.NotesTable
{
    public class Note
    {
        public int NoteID { get; set; }
        public string NoteText { get; set; }
        public string UserID { get; set; }
        public int DataID { get; set; }
        public DateTime NoteDate { get; set; }
    }
}
