using System;
using System.Collections.Generic;

namespace LASearch3
{
    public class DoubleBookingEntryVM
    {
        public Authority SelectedAuthority { get; set; }
        public SearchClerk SelectedSearchClerk { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Today;
        public List<Authority> Authority { get; set; }
        public List<SearchClerk> SearchClerk { get; set; }

        public BookingResult Result { get; set; } = new BookingResult();
    }

    public class BookingResult
    {
        public Authority SelectedAuthority { get; set; }
        public SearchClerk SelectedSearchClerk { get; set; }
        public DateTime BookingDate { get; set; }
    }
}
