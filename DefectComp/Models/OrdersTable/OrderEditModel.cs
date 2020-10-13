using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefectComp.Models.NotesTable;
using DefectComp.Models.CommonLogTable;

namespace DefectComp.Models.OrdersTable
{
    public class OrderEditModel
    {
        public Order Order { get; set; }
        public Note Note { get; set; }
        public NoteViewModel NoteViewModel {get; set;}
        public CommonLogViewModel CommonLogViewModel { get; set; }
        public int Page { get; set; }
        public string Sort { get; set; }
        public string ReturnUrl { get; set; }
        public int CurrentID { get; set; }
    }
}


