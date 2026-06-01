namespace ImportCostPro.Application.DTOs.Pais
{
    // Lo que el servicio DEVUELVE (para listar o mostrar detalle)
    namespace ImportCostPro.Application.DTOs.Pais
    {
        public class PaisDto
        {
            public int Id { get; set; }
            public string Nombre { get; set; } = string.Empty;
            public string CodigoISO { get; set; } = string.Empty; // Ej: DO, US, CN
            public bool Estado { get; set; }
        }


    }
}

