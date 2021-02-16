using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceMVC.Models
{
    public class Servicio
    {

        public Guid ServicioID { get; set; }
        public Guid EquipoID { get; set; }
        public Guid UsuarioID { get; set; }
        public DateTime FechaIng { get; set; }
        public DateTime FechaEgr { get; set; }
        public decimal TiempoTrabajo { get; set; }
        public decimal UnidadesTrabajo { get; set; }
        public decimal Total { get; set; }
        public bool Solucionado { get; set; }

        public Equipo Equipo { get; set; }
        //public AspNetUsers Usuario { get; set; }
    }
}
