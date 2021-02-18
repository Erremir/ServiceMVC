using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceMVC.Models
{
    public class RankingDiag
    {
        [Key]
        public Guid RankingDiagID { get; set; }
        public Guid ProbxDiagID { get; set; }
        public int Total { get; set; }

        public ProbxDiag ProbxDiag { get; set; }
    }
}
