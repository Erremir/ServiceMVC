using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceMVC.Models
{
    public class Diagnostico
    {
        [Key]
        public Guid DiagnosticoID { get; set; }
        public string Descripcion { get; set; }
    }
}
