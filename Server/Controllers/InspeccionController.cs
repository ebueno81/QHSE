using Microsoft.AspNetCore.Mvc;

namespace QHSE.Server.Controllers
{
    public class InspeccionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
