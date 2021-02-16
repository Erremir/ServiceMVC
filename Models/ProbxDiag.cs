using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceMVC.Models
{
    public class ProbxDiag
    {
        public Guid ProbxDiagID { get; set; }
        public Guid ProblemaID { get; set; }
        public Guid DiagnosticoID { get; set; }

        public Problema Problema { get; set; }
        public Diagnostico Diagnostico { get; set; }
    }
}
