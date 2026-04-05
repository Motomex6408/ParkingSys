using Entity.General;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Facturacion
{
    [Table("Factura")]
    public class eFactura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Ticket")]
        public int IdTicket { get; set; }

        [Required]
        [Display(Name = "Cliente")]
        public int IdCliente { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Folio")]
        public string Folio { get; set; }

        [StringLength(20)]
        [Display(Name = "Serie")]
        public string Serie { get; set; }

        [Required]
        [Display(Name = "Fecha de Emisión")]
        public DateTime FechaEmision { get; set; }

        [Display(Name = "Subtotal")]
        public decimal Subtotal { get; set; }

        [Display(Name = "Impuesto")]
        public decimal Impuesto { get; set; }

        [Display(Name = "Total")]
        public decimal Total { get; set; }

        [StringLength(30)]
        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [StringLength(100)]
        [Display(Name = "UUID Fiscal")]
        public string UUIDFiscal { get; set; }

        [ForeignKey("IdTicket")]
        public eTicket Ticket { get; set; }

        [ForeignKey("IdCliente")]
        public eCliente Cliente { get; set; }
    }
}
