using System.ComponentModel.DataAnnotations;
using Persistance.Common;

namespace Persistance.Entities
{
    public class Importador : BasicEntity<int>
    {
        [Required, StringLength(150)]
        public required string NombreRazonSocial { get; set; }

        [Required, StringLength(21)]
        public required string RNC { get; set; }

        public required int PaisID { get; set; }
        public Pais? Pais { get; set; }

        [StringLength(20)]
        public string? Telefono { get; set; }

        [StringLength(100)]
        public string? Correo { get; set; }

        [StringLength(250)]
        public string? Direccion { get; set; }
    }
}