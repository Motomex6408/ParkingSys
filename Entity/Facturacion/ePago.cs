using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Facturacion
{
    [Table("Pago")]
    public class ePago
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Factura")]
        public int IdFactura { get; set; }

        [Required]
        [Display(Name = "Método de Pago")]
        public int IdMetodoPago { get; set; }

        [Display(Name = "Monto")]
        public decimal Monto { get; set; }

        [Required]
        [Display(Name = "Fecha de Pago")]
        public DateTime FechaPago { get; set; }

        [StringLength(60)]
        [Display(Name = "Referencia")]
        public string Referencia { get; set; }

        [StringLength(30)]
        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [ForeignKey("IdFactura")]
        public eFactura Factura { get; set; }

        [ForeignKey("IdMetodoPago")]
        public eMetodoPago MetodoPago { get; set; }
    }
}
