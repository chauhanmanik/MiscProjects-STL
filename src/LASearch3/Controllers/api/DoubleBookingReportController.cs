using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace LASearch3.Controllers.api
{
    [Route("api/DoubleBookingReport")]
    public class DoubleBookingReportController : Controller
    {
        private IStlContextRepository _repository;
        private StlContext _context;

        public DoubleBookingReportController(IStlContextRepository repository, StlContext context)
        {
            _repository = repository;
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Get([FromQuery]string param1)
        {
            IEnumerable<AppointmentReportVM> model = null;
            if (string.IsNullOrEmpty(param1))
            {
                model = _repository.AppointmentReportAll();
            }
            else
            {
                DateTime date = DateTime.Parse(param1);
                model = _repository.AppointmentReportByMonth(date);
            }

            return Ok(model);
        }
    }
}
