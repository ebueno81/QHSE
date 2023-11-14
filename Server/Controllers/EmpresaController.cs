using Microsoft.AspNetCore.Mvc;

namespace QHSE.Server.Controllers
{
    public class EmpresaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
