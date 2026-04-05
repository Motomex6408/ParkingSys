using Entity.Estructura;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Administrador
{
    [Table("Usuario")]
    public class eUsuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Sucursal")]
        public int IdSucursal { get; set; }

        [Required]
        [Display(Name = "Rol")]
        public int IdRol { get; set; }

        [Required]
        [StringLength(60)]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(60)]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; }

        [StringLength(200)]
        [Display(Name = "Foto")]
        public string Foto { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Correo")]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Clave")]
        public string Password { get; set; }

        [Display(Name = "Activo")]
        public bool Activo { get; set; }

        [ForeignKey("IdSucursal")]
        public eSucursal Sucursal { get; set; }

        [ForeignKey("IdRol")]
        public eRol Rol { get; set; }
    }
}
