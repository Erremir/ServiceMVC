using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceMVC.Models
{
    public class Problema
    {
        [Key]
        public Guid ProblemaID { get; set; }
        public string Descripcion { get; set; }
    }
}
