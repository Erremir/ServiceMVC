using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceMVC.Models
{
    public class RankingSol
    {
        [Key]
        public Guid RankingSolID { get; set; }
        public Guid DiagxSolID { get; set; }
        public int Total { get; set; }

        public DiagxSol DiagxSol { get; set; }
    }
}
