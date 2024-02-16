using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmpresaTVxCable.Models;

public partial class Servicio
{
    public int IdServicio { get; set; }

    public string Nombre { get; set; } = null!;

    public int Precio { get; set; }

    public virtual ICollection<Contrato> Contratos { get; set; } = new List<Contrato>();
}
