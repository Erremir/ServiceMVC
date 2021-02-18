using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceMVC.Models
{
    public class Costo
    {
        [Key]
        public Guid CostoID { get; set; }
        public Guid SolucionID { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Tiempo { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Unidades { get; set; }

        public Solucion Solucion { get; set; }

    }
}
