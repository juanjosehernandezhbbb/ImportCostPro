using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportCostPro.Application.DTOs.Importadores
{
    public class CrearImportadorDto
    {
        public string NombreRazonSocial { get; set; } = string.Empty;
        public string RNC { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public int PaisID { get; set; }
    }
}

