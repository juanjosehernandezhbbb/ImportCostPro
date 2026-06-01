using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportCostPro.Application.DTOs.Moneda
{
    public class CrearMonedaDTO
    {
        public string Nombre { get; set; } = string.Empty;
        public string CodigoISO { get; set; } = string.Empty; // Debe ser exactamente 3 caracteres
        public string Simbolo { get; set; } = string.Empty;
        public bool EsMonedaLocal { get; set; }
        // Nota: El Estado viene activo por defecto según las reglas.
    }
}

