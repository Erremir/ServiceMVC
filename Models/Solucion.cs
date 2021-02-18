using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceMVC.Models
{
    public class Solucion
    {
        [Key]
        public Guid SolucionID { get; set; }
        [Required]
        public string Descripcion { get; set; }
    }
}
