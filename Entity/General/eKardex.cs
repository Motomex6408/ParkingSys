using Entity.Estructura;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.General
{
    [Table("Kardex")]
    public class eKardex
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Ticket")]
        public int IdTicket { get; set; }

        [Required]
        [Display(Name = "Tipo Movimiento")]
        [StringLength(50)]
        public string TipoMovimiento { get; set; }      

        [Required]
        [Display(Name = "Fecha")]
        public DateTime Fecha { get; set; }

        [Display(Name = "Espacio Anterior")]
        public int? IdEspacioAnterior { get; set; }

        [Display(Name = "Espacio Nuevo")]
        public int? IdEspacioNuevo { get; set; }

        [StringLength(150)]
        [Display(Name = "Observación")]
        public string Observacion { get; set; }

        [ForeignKey("IdTicket")]
        public eTicket Ticket { get; set; }

        [ForeignKey("IdEspacioAnterior")]
        public eEspacio EspacioAnterior { get; set; }

        [ForeignKey("IdEspacioNuevo")]
        public eEspacio EspacioNuevo { get; set; }
    }
}
