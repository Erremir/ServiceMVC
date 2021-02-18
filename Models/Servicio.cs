using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceMVC.Models
{
    public class Servicio
    {
        [Key]
        public Guid ServicioID { get; set; }
        public Guid EquipoID { get; set; }
        public DateTime FechaIng { get; set; }
        public DateTime FechaEgr { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TiempoTrabajo { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnidadesTrabajo { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
        public bool Solucionado { get; set; }

        public Cliente Cliente { get; set; }
    }
}
