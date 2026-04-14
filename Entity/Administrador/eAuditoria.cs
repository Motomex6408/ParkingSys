using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Administrador
{
    [Table("Auditoria")]
    public class eAuditoria
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Usuario")]
        public int IdUsuario { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Módulo")]
        public string Modulo { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Acción")]
        public string Accion { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required]
        [Display(Name = "Fecha")]
        public DateTime Fecha { get; set; }

        [ForeignKey("IdUsuario")]
        public eUsuario Usuario { get; set; }
    }
}