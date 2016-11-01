using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LASearch3
{
    public class DoubleAppointment
    {
        public int Id { get; set; }


        [ForeignKey("Authority"), Column(Order = 2, TypeName = "int")]
        public int AuthorityID { get; set; }
        public virtual Authority Authority { get; set; }


        [ForeignKey("SearchClerk"), Column(Order = 3, TypeName = "int")]
        public int SearchClerkID { get; set; }
        public virtual SearchClerk SearchClerk { get; set; }


        public DateTime CreatedDate { get; set; }
    }
}
