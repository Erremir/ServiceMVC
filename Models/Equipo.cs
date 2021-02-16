﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceMVC.Models
{
    public class Equipo
    {
        public Guid EquipoID { get; set; }
        public Guid ClienteID { get; set; }
        public Guid CategoriaEquipoID { get; set; }
        public string Descripcion { get; set; }

        public Cliente Cliente { get; set; }
        public CategoriaEquipo CategoriaEquipo { get; set; }
    }
}
