namespace MiTiendaVirtual.Web.Models
{
    public class ProductoDto
    {
        public int Id { get; set; }

        public int? IdCategoria { get; set; }

        public int? IdMarca { get; set; }

        public string? Nombre { get; set; }

        public string? Descripcion { get; set; }

        public decimal? Precio { get; set; }

        public string? Url { get; set; }

        public bool? Destacado { get; set; }

        public bool? Activo { get; set; }

        //Este es un Cambio de Persona 2
    }
}
