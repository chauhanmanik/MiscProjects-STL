using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LASearch3
{
    public class Authority
    {
        //public Authority()
        //{
        //    this.DoubleAppointments = new HashSet<DoubleAppointment>();
        //}
        public int Id { get; set; }
        public string Name { get; set; }
                
        public virtual ICollection<DoubleAppointment> DoubleAppointments { get; set; }
    }
}
