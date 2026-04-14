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
        public int IdTicket { get; set; }

        [Required]
        [StringLength(20)]
        public string TipoMovimiento { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        public decimal Monto { get; set; }

        [StringLength(200)]
        public string Descripcion { get; set; }

        [ForeignKey("IdTicket")]
        public eTicket Ticket { get; set; }
    }
}
