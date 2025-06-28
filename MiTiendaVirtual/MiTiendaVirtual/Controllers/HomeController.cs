using Microsoft.AspNetCore.Mvc;
using MiTiendaVirtual.Models;
using System.Diagnostics;

namespace MiTiendaVirtual.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public string Texto()
        {
            using (TiendaVirtualDbContext _dbcontext = new TiendaVirtualDbContext())
            {
                return string.Join(",",_dbcontext.Productos.Select(x=>x.Nombre));
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
