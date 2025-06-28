using System;
using System.Collections.Generic;

namespace MiTiendaVirtual.Models;

public partial class Pedido
{
    public int Id { get; set; }

    public int? IdCliente { get; set; }

    public int? IdTarjeta { get; set; }

    public DateTime? FechaHora { get; set; }

    public string? Estado { get; set; }

    public decimal? Total { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }

    public virtual Tarjetum? IdTarjetaNavigation { get; set; }

    public virtual ICollection<PedidoDetalle> PedidoDetalles { get; set; } = new List<PedidoDetalle>();
}
