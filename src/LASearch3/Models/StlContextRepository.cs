using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LASearch3
{
    public class StlContextRepository : IStlContextRepository
    {
        private StlContext _context;
        private IHostingEnvironment _env;

        public StlContextRepository(StlContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        //Authority
        public List<Authority> GetAuthorityAll()
        {
            return _context.Authorities.ToList();
        }

        //Search Clerk
        public List<SearchClerk> GetSearchClerkAll()
        {
            return _context.SearchClerks.ToList();
        }

        public SearchClerk GetSearchClerkByID(int ID)
        {
            return _context.SearchClerks.Where(x => x.Id == ID).FirstOrDefault();
        }

        public List<DoubleAppointment> GetDoubleAppointmentAll()
        {
            return _context.DoubleAppointments.ToList();
        }
        public DoubleAppointment GetDoubleAppointmentByID(int id)
        {
            return _context.DoubleAppointments.Where(x => x.Id == id).FirstOrDefault();
        }

        public List<DoubleAppointment> GetDoubleAppointmentByDate(DateTime date)
        {
            return _context.DoubleAppointments.Where(x => x.CreatedDate == date).ToList();
        }

        //Add Double booking appointment to Entity
        public void AddDoubleAppontment(BookingResult entry)
        {
            using (StlContext _con = new StlContext(_env))
            {
                var authData = _con.Authorities.Single(x => x.Id == entry.SelectedAuthority.Id);
                var clerkSet = _con.SearchClerks.Single(x => x.Id == entry.SelectedSearchClerk.Id);
                var appointmentSet = _con.Set<DoubleAppointment>();
                DoubleAppointment data = new DoubleAppointment()
                {
                    Authority = authData,
                    SearchClerk = clerkSet,
                    CreatedDate = entry.BookingDate
                };

                appointmentSet.Add(data);
                _con.SaveChanges();
            }
        }

        //Get Booking Entry data for the booking form
        public DoubleBookingEntryVM GetDoubleBookingEntryVM()
        {
            return new DoubleBookingEntryVM() { Authority = GetAuthorityAll(), SearchClerk = GetSearchClerkAll() };
        }

        //Double booking report
        public List<AppointmentReportVM> AppointmentReportAll()
        {
            //*********** ALERT: A Quick Fix for EFCore to populate virtual properties ******
            //*********** TODO: Investigate for the fix                                ******
            var lazyLoad = _context.Authorities.Select(x => x.DoubleAppointments.Select(y=>y.Authority)).ToList();
            var lazyLoad2 = _context.DoubleAppointments.Select(x => x.Authority).ToList();

            var lazyLoad3 = _context.SearchClerks.Select(x => x.DoubleAppointments.Select(y => y.SearchClerk)).ToList();
            var lazyLoad4 = _context.DoubleAppointments.Select(x => x.SearchClerk).ToList();

            //********************************************************************************

            List<DoubleAppointment> bookings = _context.DoubleAppointments.ToList();
            List<AppointmentReportVM> report = new List<AppointmentReportVM>();

            var group = from item in bookings
                        group item by item.Authority into grp
                        select new
                        {
                            name = grp.Key.Name,
                            count = grp.Count(),
                            bookingDate = grp.Key.DoubleAppointments.Select(x=>x.CreatedDate).ToList(),
                            searchClerk = grp.Key.DoubleAppointments.Select(x=>x.SearchClerk).ToList()
                        };

            foreach(var item in group)
            {
                var appointment = new AppointmentReportVM()
                {
                    AuthorityName = item.name,
                    NumberOfBookings = item.count,
                    BookingDate = item.bookingDate,
                    SearchClerks = item.searchClerk.Select(x=> new SearchClerkVM(){ ID =  x.Id, Email=x.Email, Name = x.Name }).ToList()
                };

                report.Add(appointment);
            }

            return report;
        }
        
        //Report by month
        public List<AppointmentReportVM> AppointmentReportByMonth(DateTime date)
        {
            //*********** ALERT: A Quick Fix for EFCore to populate virtual properties ******
            //*********** TODO: Investigate for the fix                                ******
            var lazyLoad = _context.Authorities.Select(x => x.DoubleAppointments.Select(y => y.Authority)).ToList();
            var lazyLoad2 = _context.DoubleAppointments.Select(x => x.Authority).ToList();

            var lazyLoad3 = _context.SearchClerks.Select(x => x.DoubleAppointments.Select(y => y.SearchClerk)).ToList();
            var lazyLoad4 = _context.DoubleAppointments.Select(x => x.SearchClerk).ToList();

            //********************************************************************************

            List<DoubleAppointment> bookings = _context.DoubleAppointments
                                                       .Where(x=>x.CreatedDate.Month==date.Month && x.CreatedDate.Year==date.Year).ToList();
            List<AppointmentReportVM> report = new List<AppointmentReportVM>();

            var group = from item in bookings
                        group item by item.Authority into grp
                        select new
                        {
                            name = grp.Key.Name,
                            count = grp.Count(),
                            bookingDate = grp.Key.DoubleAppointments.Select(x => x.CreatedDate).ToList(),
                            searchClerk = grp.Key.DoubleAppointments.Select(x => x.SearchClerk).ToList()
                        };

            foreach (var item in group)
            {
                var appointment = new AppointmentReportVM()
                {
                    AuthorityName = item.name,
                    NumberOfBookings = item.count,
                    BookingDate = item.bookingDate,
                    SearchClerks = item.searchClerk.Select(x => new SearchClerkVM() { ID = x.Id, Email = x.Email, Name = x.Name }).ToList()
                };

                report.Add(appointment);
            }

            return report;
        }
    }
}
