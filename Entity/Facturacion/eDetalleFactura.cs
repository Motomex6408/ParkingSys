using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Facturacion
{
    [Table("DetalleFactura")]
    public class eDetalleFactura
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Factura")]
        public int IdFactura { get; set; }

        [Required]
        [StringLength(150)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Cantidad")]
        public decimal Cantidad { get; set; }

        [Display(Name = "Precio Unitario")]
        public decimal PrecioUnitario { get; set; }

        [Display(Name = "Subtotal")]
        public decimal Subtotal { get; set; }

        [ForeignKey("IdFactura")]
        public eFactura Factura { get; set; }
    }
}
