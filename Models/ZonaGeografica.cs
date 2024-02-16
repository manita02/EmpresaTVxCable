using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmpresaTVxCable.Models;

public partial class ZonaGeografica
{
    public int IdZona { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdRegion { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    //public virtual ICollection<Cliente> Clientes { get; set; }

    public virtual Region IdRegionNavigation { get; set; } = null!;
}
