using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceMVC.Models
{
    public class CategoriaEquipo
    {
        [Key]
        public Guid CategoriaEquipoID { get; set; }

        public string Categoria { get; set; }

    }
}
