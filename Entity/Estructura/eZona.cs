using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Estructura
{
    [Table("Zona")]
    public class eZona
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Sucursal")]
        public int IdSucursal { get; set; }

        [Required]
        [StringLength(60)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [StringLength(150)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Nivel")]
        public int Nivel { get; set; }

        [ForeignKey("IdSucursal")]
        public eSucursal Sucursal { get; set; }
    }
}
