using Microsoft.AspNetCore.Mvc;

namespace LASearch3.Controllers
{
    public class HomeController : Controller
    {
        private IStlContextRepository _contextRepository;

        public HomeController(IStlContextRepository context)
        {
            _contextRepository = context;
        }
        public IActionResult Index()
        {
            var model = _contextRepository.GetDoubleBookingEntryVM();
            return View(model);
        }
        
        public IActionResult Report()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
