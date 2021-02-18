using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ServiceMVC.Models
{
    public class ServxUsuario
    {
        [Key]
        public Guid ServcUsuarioID { get; set; }
        public string UsuarioID { get; set; }
        public Guid ServicioID { get; set; }
        public bool Status { get; set; }

        public Servicio Servicio { get; set; }
        public ApplicationUser Usuario { get; set; }
    }
}
