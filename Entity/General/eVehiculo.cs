using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.General
{
    [Table("Vehiculo")]
    public class eVehiculo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Placa")]
        public string Placa { get; set; }

        [Required]
        [Display(Name = "Tipo de Vehículo")]
        public int IdTipoVehiculo { get; set; }

        [StringLength(60)]
        [Display(Name = "Marca")]
        public string Marca { get; set; }

        [StringLength(60)]
        [Display(Name = "Modelo")]
        public string Modelo { get; set; }

        [StringLength(30)]
        [Display(Name = "Color")]
        public string Color { get; set; }

        [ForeignKey("IdTipoVehiculo")]
        public eTipoVehiculo TipoVehiculo { get; set; }
    }
}
