using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceMVC.Models
{
    public class RankingDiag
    {

        public Guid RankingDiagID { get; set; }
        public Guid ProbxDiagID { get; set; }
        public int Total { get; set; }

        public ProbxDiag ProbxDiag { get; set; }
    }
}
