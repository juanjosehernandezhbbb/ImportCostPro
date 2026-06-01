using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance.Common;

namespace Persistance.Entities
{
    public class Importador: BasicEntity<int>
    {
        public required string NombreRazonSocial { get; set; }
        public required string RNC { get; set; }
        public required int PaisID { get; set; } // fk - match Pais.Id (int)
        public Pais? Pais { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }

        public string? Direccion { get; set; }
    }
}
