using ImportCostPro.Application.DTOs.Impuestos;

namespace ImportCostPro.Application.Interfaces
{
    public interface IConfigImpuestosService
    {
        Task<ConfiguracionImpuestosDto> ObtenerConfiguracionAsync();
        Task GuardarConfiguracionAsync(ConfiguracionImpuestosCreateDto dto);
    }
}