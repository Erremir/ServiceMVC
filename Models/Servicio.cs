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
        public Guid ClienteID { get; set; }
        public Guid EquipoID { get; set; }
        [Display(Name = "Fecha Ingreso")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaIng { get; set; }
        [Display(Name = "Fecha Egreso")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? FechaEgr { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? TiempoTrabajo { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? UnidadesTrabajo { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Total { get; set; }
        public string Comentarios { get; set; }
        public bool Solucionado { get; set; }
        public bool Finalizado { get; set; }

        public Cliente Cliente { get; set; }
    }
}
