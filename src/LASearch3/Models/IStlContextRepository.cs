using System;
using System.Collections.Generic;

namespace LASearch3
{
    public interface IStlContextRepository
    {
        List<DoubleAppointment> GetDoubleAppointmentAll();
        List<DoubleAppointment> GetDoubleAppointmentByDate(DateTime date);
        DoubleAppointment GetDoubleAppointmentByID(int id);
        List<Authority> GetAuthorityAll();
        List<SearchClerk> GetSearchClerkAll();
        DoubleBookingEntryVM GetDoubleBookingEntryVM();
        void AddDoubleAppontment(BookingResult doubleBookingEntryVM);
        List<AppointmentReportVM> AppointmentReportAll();
        List<AppointmentReportVM> AppointmentReportByMonth(DateTime date);
    }
}