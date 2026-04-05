using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Estructura
{
    [Table("Sucursal")]
    public class eSucursal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Empresa")]
        public int IdEmpresa { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }

        [StringLength(30)]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [StringLength(100)]
        [Display(Name = "Correo")]
        public string Email { get; set; }

        [Display(Name = "Capacidad Total")]
        public int CapacidadTotal { get; set; }

        [Display(Name = "Activo")]
        public bool Activo { get; set; }

        [ForeignKey("IdEmpresa")]
        public eEmpresa Empresa { get; set; }
    }
}
