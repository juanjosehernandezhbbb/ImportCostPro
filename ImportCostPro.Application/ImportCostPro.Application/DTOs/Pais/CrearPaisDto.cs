using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportCostPro.Application.DTOs.Pais
{
    // Lo que el servicio RECIBE (para crear o editar)
    public class CrearPaisDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string CodigoISO { get; set; } = string.Empty;
    }
}
