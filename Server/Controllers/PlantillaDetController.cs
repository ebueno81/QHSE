using Microsoft.AspNetCore.Mvc;

namespace QHSE.Server.Controllers
{
    public class PlantillaDetController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
