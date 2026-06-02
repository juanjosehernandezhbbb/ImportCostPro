using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance.Common;

namespace Persistance.Entities
{
    public class Proveedor : BasicEntity<int>
    {
        public required string Nombre { get; set; }
        public required int PaisOrigenID { get; set; }
        public Pais? PaisOrigen { get; set; }
        public required int MonedaPrincipalID { get; set; }
        public Moneda? MonedaPrincipal { get; set; }
        public string? Telefono { get; set; }

       
        public string? Correo { get; set; } 
        public string? Direccion { get; set; } 
    }
}