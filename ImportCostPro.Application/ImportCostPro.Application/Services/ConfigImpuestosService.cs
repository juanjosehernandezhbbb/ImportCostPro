using ImportCostPro.Application.DTOs.Impuestos;
using ImportCostPro.Application.Interfaces;
using Persistence.Contexts;
using Persistance.Entities; // Ajustado a 'Persistance' con 'a' como en tu contexto
using Microsoft.EntityFrameworkCore;

namespace ImportCostPro.Application.Services
{
    public class ConfigImpuestosService : IConfigImpuestosService
    {
        private readonly ImportCostProContext _context;

        public ConfigImpuestosService(ImportCostProContext context) => _context = context;

        public async Task<ConfiguracionImpuestosDto> ObtenerConfiguracionAsync()
        {
            var config = await _context.ConfiguracionImpuestos.FirstOrDefaultAsync();
            if (config == null) return null;

            return new ConfiguracionImpuestosDto
            {
                Id = config.Id,
                PorcentajeITBIS = config.PorcentajeITBIS,
                // Ajustado al nombre real de tu entidad
                PorcentajeTasaServicioAduanal = config.PorcentajeTasaAduanal
            };
        }

        public async Task GuardarConfiguracionAsync(ConfiguracionImpuestosCreateDto dto)
        {
            var config = await _context.ConfiguracionImpuestos.FirstOrDefaultAsync();

            if (config == null)
            {
                _context.ConfiguracionImpuestos.Add(new ConfiguracionImpuestos
                {
                    PorcentajeITBIS = dto.PorcentajeITBIS,
                    // Ajustado al nombre real de tu entidad
                    PorcentajeTasaAduanal = dto.PorcentajeTasaServicioAduanal
                });
            }
            else
            {
                config.PorcentajeITBIS = dto.PorcentajeITBIS;
                // Ajustado al nombre real de tu entidad
                config.PorcentajeTasaAduanal = dto.PorcentajeTasaServicioAduanal;
            }

            await _context.SaveChangesAsync();
        }
    }
}