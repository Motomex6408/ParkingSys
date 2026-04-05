using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Estructura
{
    [Table("Empresa")]
    public class eEmpresa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "RTN")]
        public string RTN { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Dirección Fiscal")]
        public string DireccionFiscal { get; set; }

        [StringLength(30)]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [StringLength(100)]
        [Display(Name = "Correo")]
        public string Email { get; set; }

        [Display(Name = "Activo")]
        public bool Activo { get; set; }
    }
}
