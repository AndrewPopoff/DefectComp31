using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DefectComp.Models.CommonLogTable
{
    [Table("CommonLog")]
    public class CommonLog
    {
        [Key]
        public int RecordID { get; set; }
        public string Action { get; set; }
        public int ActionID { get; set; }
        public string TxtMsg { get; set; }
        public Guid _UID { get; set; }
        public DateTime ActionDate { get; set; }
        public string UserDesc { get; set; }
    }
}
