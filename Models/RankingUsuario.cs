﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ServiceMVC.Models
{
    public class RankingUsuario
    {
        [Key]
        public Guid RankingUsuarioID { get; set; }
        public string UsuarioID { get; set; }
        public int EnProceso { get; set; }
        public int Solucionados { get; set; }
        public int Fallados { get; set; }

        public ApplicationUser Usuario { get; set; }
    }
}
