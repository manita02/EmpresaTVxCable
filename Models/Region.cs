using System;
using System.Collections.Generic;

namespace EmpresaTVxCable.Models;

public partial class Region
{
    public int IdRegion { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<ZonaGeografica> ZonaGeograficas { get; set; } = new List<ZonaGeografica>();
}
