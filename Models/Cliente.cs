using System;
using System.Collections.Generic;

namespace EmpresaTVxCable.Models;

public partial class Cliente
{
    public int IdCliente { get; set; }

    public string Nombre { get; set; } = null!;

    public int Dni { get; set; }

    public bool Activo { get; set; }

    public int IdZona { get; set; }

    public virtual ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();

    public virtual ZonaGeografica IdZonaNavigation { get; set; } = null!;
}
