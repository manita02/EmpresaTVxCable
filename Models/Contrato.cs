using System;
using System.Collections.Generic;

namespace EmpresaTVxCable.Models;

public partial class Contrato
{
    public int IdContrato { get; set; }

    public int IdServicio { get; set; }

    public int IdCliente { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Servicio IdServicioNavigation { get; set; } = null!;
}
