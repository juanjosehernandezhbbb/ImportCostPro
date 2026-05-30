namespace ImportCostPro.Data.DTOs.Pais
{
    // Lo que el servicio DEVUELVE (para listar o mostrar detalle)
    public class PaisDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string CodigoISO { get; set; }
        public bool Activo { get; set; }
    }

    // Lo que el servicio RECIBE (para crear o editar)
    public class PaisCreateDto
    {
        public string Nombre { get; set; }
        public string CodigoISO { get; set; }
        public bool Activo { get; set; }
    }
}

