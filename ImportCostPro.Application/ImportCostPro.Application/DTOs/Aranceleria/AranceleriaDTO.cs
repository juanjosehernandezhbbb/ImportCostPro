namespace ImportCostPro.Application.DTOs.Aranceleria
{
    // Lo que el servicio DEVUELVE
    public class CategoriaArancelariaDto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public decimal PorcentajeArancel { get; set; }
        public bool AplicaITBIS { get; set; }
        public bool AplicaImpuestoSelectivo { get; set; }
        public decimal PorcentajeImpuestoSelectivo { get; set; }
        public bool Activo { get; set; }
    }

    // Lo que el servicio RECIBE
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