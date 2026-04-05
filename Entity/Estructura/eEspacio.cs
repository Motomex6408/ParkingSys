using Entity.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Estructura
{
    [Table("Espacio")]
    public class eEspacio
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Zona")]
        public int IdZona { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [Required]
        [Display(Name = "Tipo de Vehículo")]
        public int IdTipoVehiculo { get; set; }

        [Display(Name = "Disponible")]
        public bool Disponible { get; set; }

        [ForeignKey("IdZona")]
        public eZona Zona { get; set; }

        [ForeignKey("IdTipoVehiculo")]
        public eTipoVehiculo TipoVehiculo { get; set; }
    }
}