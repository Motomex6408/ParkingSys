using Entity.Estructura;
using Entity.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Administrador
{
    [Table("Tarifa")]
    public class eTarifa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Sucursal")]
        public int IdSucursal { get; set; }

        [Required]
        [Display(Name = "Tipo de Vehículo")]
        public int IdTipoVehiculo { get; set; }

        [Required]
        [StringLength(60)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Precio por Hora")]
        public decimal PrecioHora { get; set; }

        [Required]
        [Display(Name = "Precio por Día")]
        public decimal PrecioDia { get; set; }

        [Required]
        [Display(Name = "Precio por Mes")]
        public decimal PrecioMes { get; set; }

        [Required]
        [Display(Name = "Vigencia Inicio")]
        public DateTime VigenciaInicio { get; set; }

        [Required]
        [Display(Name = "Vigencia Fin")]
        public DateTime VigenciaFin { get; set; }

        [ForeignKey("IdSucursal")]
        public eSucursal Sucursal { get; set; }

        [ForeignKey("IdTipoVehiculo")]
        public eTipoVehiculo TipoVehiculo { get; set; }
    }
}