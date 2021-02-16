using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceMVC.Models
{
    public class RankingClientes
    {

        public Guid RankingClientesID { get; set; }
        public Guid ClienteID { get; set; }
        public int TotalServicios { get; set; }

        public Cliente Cliente { get; set; }
    }
}
