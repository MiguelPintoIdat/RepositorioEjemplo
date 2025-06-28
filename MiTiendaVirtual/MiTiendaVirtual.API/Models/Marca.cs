using System;
using System.Collections.Generic;

namespace MiTiendaVirtual.API.Models;

public partial class Marca
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
