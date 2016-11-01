using System;
using System.Collections.Generic;

namespace LASearch3
{
    public class AppointmentReportVM
    {
        public string AuthorityName { get; set; }
        public int NumberOfBookings { get; set; }
        public List<DateTime> BookingDate { get; set; }
        public List<SearchClerkVM> SearchClerks { get; set; }
    }
}
