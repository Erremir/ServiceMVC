using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceMVC.Models
{
    public class RankingClientes
    {
        [Key]
        public Guid RankingClientesID { get; set; }
        public Guid ClienteID { get; set; }
        public int TotalServicios { get; set; }

        public Cliente Cliente { get; set; }
    }
}
