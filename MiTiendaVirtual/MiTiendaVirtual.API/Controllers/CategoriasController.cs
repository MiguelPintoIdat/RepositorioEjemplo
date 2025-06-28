using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiTiendaVirtual.API.Models;

namespace MiTiendaVirtual.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        [HttpGet("Obtener")]
        public async Task<List<Categorium>> ObtenerCategorias()
        {
            var lista = new List<Categorium>();

            using (TiendaVirtualDbContext _dbcontext = new TiendaVirtualDbContext())
            {
                lista = await _dbcontext.Categoria.ToListAsync();
            }

            return lista;
        }
        [HttpGet("ObtenerCombo")]
        public async Task<List<Categorium>> ObtenerComboCategorias()
        {
            var lista = new List<Categorium>();

            using (TiendaVirtualDbContext _dbcontext = new TiendaVirtualDbContext())
            {
                lista = await _dbcontext.Categoria.Where(x=>x.Activo??false).ToListAsync();
            }

            return lista;
        }
        [HttpPost("Registrar")]
        public async Task<string> RegistrarCategoria(Categorium data)
        {
            var mensaje = "";

            // Validar mis campos

            // 1. Precio tiene que ser mayor que 0

            if (data.Nombre.Length <= 0)
            {
                mensaje = "El nombre es obligatorio.";
                return mensaje;
            }


            using (TiendaVirtualDbContext _dbcontext = new TiendaVirtualDbContext())
            {

                // 2. Validar que el nombre de la categoría no se repita
                // Zapatillas ---- ZAPATILLAS

                var repetido = await _dbcontext.Categoria.AnyAsync(x => x.Nombre.ToUpper() == data.Nombre.ToUpper());

                if (repetido)
                {
                    mensaje = "Ya existe una categoría con el mismo nombre";
                    return mensaje;
                }

                data.Activo = true;

                await _dbcontext.Categoria.AddAsync(data);

                await _dbcontext.SaveChangesAsync();

                mensaje = "Se registró la categoría.";
            }

            return mensaje;
        }

        [HttpDelete("Eliminar")]
        public async Task<string> EliminarCategoria(int Id)
        {
            var mensaje = "";

            using (TiendaVirtualDbContext _dbcontext = new TiendaVirtualDbContext())
            {
                // 1. Buscar en la BD aquel que quiero eliminar

                var cate = await _dbcontext.Categoria.FindAsync(Id);


                // Verificamos si el prod es null. Si es null, detenemos la operación.

                if (cate == null)
                {
                    mensaje = "Categoría no encontrada.";
                    return mensaje;
                }


                // 2. Eliminamos el producto que encontramos

                _dbcontext.Categoria.Remove(cate);

                // 3. Guardamos los cambios

                await _dbcontext.SaveChangesAsync();

                // 4. Escribimos Mensaje

                mensaje = "Categoría eliminada";
            }



            return mensaje;
        }
        [HttpPost("Actualizar")]
        public async Task<string> ActualizarCategoria(Categorium data)
        {
            var mensaje = "";

            // Validar mis campos

            // 1. Precio tiene que ser mayor que 0

            if (data.Nombre.Length <= 0)
            {
                mensaje = "El nombre es obligatorio.";
                return mensaje;
            }


            using (TiendaVirtualDbContext _dbcontext = new TiendaVirtualDbContext())
            {

                // 2. Validar que el nombre de la categoría no se repita
                // Zapatillas ---- ZAPATILLAS

                var repetido = await _dbcontext.Categoria.AnyAsync(x => x.Nombre.ToUpper() == data.Nombre.ToUpper() & x.Id != data.Id);

                if (repetido)
                {
                    mensaje = "Ya existe una categoría con el mismo nombre";
                    return mensaje;
                }

                // Buscar ese producto en la Base de Datos

                var cate = await _dbcontext.Categoria.FindAsync(data.Id);

                // Chancar los nuevos valores según la data del parámetro

                cate.Nombre = data.Nombre;

                await _dbcontext.SaveChangesAsync();

                mensaje = "Se actualizó la categoría.";
            }

            return mensaje;
        }

    }
}
