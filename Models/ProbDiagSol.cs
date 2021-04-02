using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceMVC.Models
{
    public class ProbDiagSol
    {
        [Key]
        public Guid ProbDiagSolID { get; set; }
        public Guid ServicioID { get; set; }
        public Guid? ProblemaID { get; set; }
        public Guid? DiagnosticoID { get; set; }
        public Guid? SolucionID { get; set; }

        public Servicio Servicio { get; set; }
        public Problema Problema { get; set; }
        public Diagnostico Diagnostico { get; set; }
        public Solucion Solucion { get; set; }
    }
}
