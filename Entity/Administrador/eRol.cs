using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Administrador
{
    [Table("Rol")]
    public class eRol
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(60)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [StringLength(150)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
    }
}
