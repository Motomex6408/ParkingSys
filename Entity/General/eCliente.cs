using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.General
{
    [Table("Cliente")]
    public class eCliente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(60)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(60)]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; }

        [StringLength(30)]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [StringLength(100)]
        [Display(Name = "Correo")]
        public string Email { get; set; }

        [StringLength(30)]
        [Display(Name = "RTN")]
        public string RTN { get; set; }

        [StringLength(30)]
        [Display(Name = "Tipo")]
        public string Tipo { get; set; }
    }
}
