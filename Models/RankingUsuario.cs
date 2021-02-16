using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceMVC.Models
{
    public class RankingUsuario
    {

        public Guid RankingUsuarioID { get; set; }
        public Guid UsuarioID { get; set; }
        public int EnProceso { get; set; }
        public int Solucionados { get; set; }
        public int Fallados { get; set; }

        //public AspNetUsers Usuario { get; set; }
    }
}
