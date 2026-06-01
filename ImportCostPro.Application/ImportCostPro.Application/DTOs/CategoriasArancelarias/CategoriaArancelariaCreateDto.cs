using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportCostPro.Application.DTOs.CategoriasArancelarias
{
    public class CategoriaArancelariaCreateDto
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal PorcentajeArancel { get; set; }
        public bool AplicaITBIS { get; set; }
        public bool AplicaImpuestoSelectivo { get; set; }
        public decimal PorcentajeImpuestoSelectivo { get; set; }
        public bool Activo { get; set; }
    }
}
