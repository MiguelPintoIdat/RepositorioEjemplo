using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiTiendaVirtual.API.Models;

namespace MiTiendaVirtual.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcasController : ControllerBase
    {
        [HttpGet("ObtenerCombo")]
        public async Task<List<Marca>> ObtenerComboMarcas()
        {
            var lista = new List<Marca>();

            using (TiendaVirtualDbContext _dbcontext = new TiendaVirtualDbContext())
            {
                lista = await _dbcontext.Marcas.ToListAsync();
            }

            return lista;
        }
    }
}
