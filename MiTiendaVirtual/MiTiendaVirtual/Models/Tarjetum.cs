using System;
using System.Collections.Generic;

namespace MiTiendaVirtual.Models;

public partial class Tarjetum
{
    public int Id { get; set; }

    public string? Marca { get; set; }

    public string? Numero { get; set; }

    public virtual ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
}
