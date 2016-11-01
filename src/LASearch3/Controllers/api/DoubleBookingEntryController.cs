using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace LASearch3.Controllers.api
{
    [Route("api/DoubleBookingEntry")]
    public class DoubleBookingEntryController : Controller
    {
        private StlContext _context;
        private IStlContextRepository _repository;

        public DoubleBookingEntryController(IStlContextRepository repository, StlContext context)
        {
            _repository = repository;
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            DoubleBookingEntryVM model = _repository.GetDoubleBookingEntryVM();
            return Ok(model);
        }

        [HttpPost("")]
        public IActionResult Post([FromBody]BookingResult model)
        {
            if (ModelState.IsValid)
            {
                _repository.AddDoubleAppontment(model);

                if (_context.DoubleAppointments.LastOrDefault().Id > 0)
                {
                    return Created($"api/DoubleBookingEntry/{model.SelectedAuthority.Name},", model);
                }
            }
            return BadRequest(ModelState);
        }
    }
}
