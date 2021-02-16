using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceMVC.Models
{
    public class Costo
    {
        public Guid CostoID { get; set; }
        public Guid SolucionID { get; set; }
        public decimal Tiempo { get; set; }
        public decimal Unidades { get; set; }

        public Solucion Solucion { get; set; }

    }
}
