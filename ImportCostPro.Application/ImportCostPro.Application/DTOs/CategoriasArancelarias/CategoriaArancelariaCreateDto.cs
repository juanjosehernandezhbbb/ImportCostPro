namespace ImportCostPro.Application.DTOs.CategoriasArancelarias
{
    public class CategoriaArancelariaCreateDto
    {
        public string Codigo { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public decimal PorcentajeArancel { get; set; }
        public bool AplicaITBIS { get; set; }
        public bool AplicaImpuestoSelectivo { get; set; }
        public decimal? PorcentajeImpuestoSelectivo { get; set; }
        public bool Activo { get; set; } = true;
    }
}