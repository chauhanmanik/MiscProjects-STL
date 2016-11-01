using System.Collections.Generic;

namespace LASearch3
{
    public class SearchClerk
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public virtual ICollection<DoubleAppointment> DoubleAppointments { get; set; }
    }
}
