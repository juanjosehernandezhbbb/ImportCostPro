
namespace ImportCostPro.Data.DTOs.ConfiguracionImpuestos
{ 
    public class ConfiguracionImpuestosDto
    {
        public int Id { get; set; }
        public decimal PorcentajeITBIS { get; set; }
        public decimal PorcentajeTasaServicioAduanal { get; set; }
    }

    public class ConfiguracionImpuestosCreateDto
    {
        public decimal PorcentajeITBIS { get; set; }
        public decimal PorcentajeTasaServicioAduanal { get; set; }
    }
}
