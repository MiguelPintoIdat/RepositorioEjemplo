using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MiTiendaVirtual.Web.Models;
using System.Diagnostics;

namespace MiTiendaVirtual.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }





        public async Task<IActionResult> ListaProductos()
        {
            var httpClient = _httpClientFactory.CreateClient();

            // Usa GetFromJsonAsync directamente
            var lista = await httpClient.GetFromJsonAsync<List<ProductoDto>>($"https://localhost:7237/api/Productos/Obtener");

            if (lista != null)
            {
                return View(lista);
            }

            return View("Error");
        }

        public async Task<IActionResult> CrearProducto()
        {

            var httpClient = _httpClientFactory.CreateClient();

            // Usa GetFromJsonAsync directamente
            var listaCategorias = await httpClient.GetFromJsonAsync<List<Categorias>>($"https://localhost:7237/api/Categorias/ObtenerCombo");

            var listaCategoriasConvertida = listaCategorias.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Nombre
            }).ToList();

            listaCategoriasConvertida.Insert(0,new SelectListItem() { Value = "0", Text = "Seleccione" });


            var listaMarcas = await httpClient.GetFromJsonAsync<List<Marca>>($"https://localhost:7237/api/Marcas/ObtenerCombo");

            var listaMarcasConvertida = listaMarcas.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Nombre
            }).ToList();

            listaMarcasConvertida.Insert(0, new SelectListItem() { Value = "0", Text = "Seleccione" });


            ViewBag.Categorias = listaCategoriasConvertida;
            ViewBag.Marcas = listaMarcasConvertida;

            return View(new ProductoDto());
        }
        [HttpPost]
        public async Task<IActionResult> CrearProducto(ProductoDto prod)
        {
            var httpClient = _httpClientFactory.CreateClient();

            // Usa GetFromJsonAsync directamente
            var response = await httpClient.PostAsJsonAsync($"https://localhost:7237/api/Productos/Registrar",prod);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("ListaProductos");
            }
            else
            {
                return View(new ProductoDto());
            }

        }
    }
}
