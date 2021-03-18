using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceMVC.Models
{
    public class Cliente
    {
        [Key]
        public Guid ClienteID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Domicilio { get; set; }
        public string Email { get; set; }
        [Display(Name = "Celular")]
        public string TelCel { get; set; }
        [Display(Name = "Fijo")]
        public string TelFijo { get; set; }
        public string Comentario { get; set; }
        public bool Status { get; set; }

        [NotMapped]
        public string ApellidoNombre
        {
            get
            {
                return Apellido + ", " + Nombre;
            }
        }

    }
}
