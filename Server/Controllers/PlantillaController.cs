using Microsoft.AspNetCore.Mvc;

namespace QHSE.Server.Controllers
{
    public class PlantillaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
