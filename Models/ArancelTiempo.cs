using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceMVC.Models
{
    public class ArancelTiempo
    {
        [Key]
        public Guid ArancelTiempoID { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Precio { get; set; }

    }
}
