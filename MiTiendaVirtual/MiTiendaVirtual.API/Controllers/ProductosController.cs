using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiTiendaVirtual.API.Models;

namespace MiTiendaVirtual.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {

        /*
          
         -> RutaBase (localhost:5000)
         -> api
         -> nombre del controller
         -> nombre del método
         
         https://localhost:7237/api/Productos/Obtener


        Tipos de Métodos para API

        HTTP: GET - POST - PUT - DELETE

        GET - DELETE : No admiten cuerpo (BODY). Solo parámetros en URL
        POST - PUT: Si admiten un cuerpo (BODY) + Parámetros en URL

         */


        /* Método para obtener la Lista de Productos */

        [HttpGet("Obtener")]
        public async Task<List<Producto>> ObtenerProductos()
        {
            var lista = new List<Producto>();

            using (TiendaVirtualDbContext _dbcontext = new TiendaVirtualDbContext())
            {
                lista = await _dbcontext.Productos.ToListAsync();
            }

            return lista;
        }

        /* Método que registre un producto. Al final me muestre un mensaje de confirmación */

        [HttpPost("Registrar")]
        public async Task<string> RegistrarProducto(Producto data)
        {
            var mensaje = "";

            // Validar mis campos

            // 1. Precio tiene que ser mayor que 0

            if(data.Precio <= 0)
            {
                mensaje = "El precio debe ser mayor que 0.";
                return mensaje;
            }


            using (TiendaVirtualDbContext _dbcontext = new TiendaVirtualDbContext())
            {

                // 2. Validar que el nombre del producto no se repita
                // Zapatillas ---- ZAPATILLAS

                var repetido = await _dbcontext.Productos.AnyAsync(x => x.Nombre.ToUpper() == data.Nombre.ToUpper());

                if(repetido)
                {
                    mensaje = "Ya existe un producto con el mismo nombre";
                    return mensaje;
                }

                await _dbcontext.Productos.AddAsync(data);

                await _dbcontext.SaveChangesAsync();

                mensaje = "Se registró el producto.";
            }

            return mensaje;
        }

        /* Método que elimine un producto. Al final me muestre un mensaje de confirmación */

        [HttpDelete("Eliminar")]
        public async Task<string> EliminarProducto(int Id)
        {
            var mensaje = "";

            using (TiendaVirtualDbContext _dbcontext = new TiendaVirtualDbContext())
            {
                // 1. Buscar en la BD aquel que quiero eliminar

                var prod = await _dbcontext.Productos.FindAsync(Id);


                // Verificamos si el prod es null. Si es null, detenemos la operación.

                if (prod == null)
                {
                    mensaje = "Producto no encontrado.";
                    return mensaje;
                }


                // 2. Eliminamos el producto que encontramos

                _dbcontext.Productos.Remove(prod);

                // 3. Guardamos los cambios

                await _dbcontext.SaveChangesAsync();

                // 4. Escribimos Mensaje

                mensaje = "Producto eliminado";
            }



            return mensaje;
        }


        /* Método que actualice un producto. Al final me muestre un mensaje de confirmación */

        [HttpPost("Actualizar")]
        public async Task<string> ActualizarProducto(Producto data)
        {
            var mensaje = "";

            // Validar mis campos

            // 1. Precio tiene que ser mayor que 0

            if (data.Precio <= 0)
            {
                mensaje = "El precio debe ser mayor que 0.";
                return mensaje;
            }


            using (TiendaVirtualDbContext _dbcontext = new TiendaVirtualDbContext())
            {

                // Validamos que exista un Producto con el ID ingresado

                var existe = await _dbcontext.Productos.AnyAsync(x => x.Id == data.Id);

                if (!existe)
                {
                    mensaje = "No existe el producto ingresado.";
                    return mensaje;
                }


                // 2. Validar que el nombre del producto no se repita
                // Zapatillas ---- ZAPATILLAS

                var repetido = await _dbcontext.Productos.AnyAsync(x => x.Nombre.ToUpper() == data.Nombre.ToUpper() & x.Id != data.Id);

                if (repetido)
                {
                    mensaje = "Ya existe un producto con el mismo nombre.";
                    return mensaje;
                }



                // Buscar ese producto en la Base de Datos

                var prod = await _dbcontext.Productos.FindAsync(data.Id);

                // Chancar los nuevos valores según la data del parámetro

                prod.Nombre = data.Nombre;
                prod.IdCategoria = data.IdCategoria;
                prod.IdMarca = data.IdMarca;
                prod.Descripcion = data.Descripcion;
                prod.Precio = data.Precio;
                prod.Destacado = data.Destacado;
                prod.Activo = data.Activo;
                prod.Url = data.Url;

                // Guardamos los cambios
                await _dbcontext.SaveChangesAsync();

                // Escribimos el mensaje
                mensaje = "Se actualizó el producto.";
            }

            return mensaje;
        }


    }
}
