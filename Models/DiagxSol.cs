﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceMVC.Models
{
    public class DiagxSol
    {
        public Guid DiagxSolID { get; set; }
        public Guid DiagnosticoID { get; set; }
        public Guid SolucionID { get; set; }

        public Diagnostico Diagnostico  { get; set; }
        public Solucion Solucion { get; set; }
    }
}
