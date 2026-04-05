using Entity.Administrador;
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
    [Table("Ticket")]
    public class eTicket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Espacio")]
        public int IdEspacio { get; set; }

        [Required]
        [Display(Name = "Vehículo")]
        public int IdVehiculo { get; set; }

        [Required]
        [Display(Name = "Cliente")]
        public int IdCliente { get; set; }

        [Required]
        [Display(Name = "Tarifa")]
        public int IdTarifa { get; set; }

        [Required]
        [Display(Name = "Usuario")]
        public int IdUsuario { get; set; }

        [Required]
        [Display(Name = "Entrada")]
        public DateTime Entrada { get; set; }

        [Display(Name = "Salida")]
        public DateTime? Salida { get; set; }

        [Display(Name = "Minutos Totales")]
        public int MinutosTotales { get; set; }

        [Display(Name = "Monto Calculado")]
        public decimal MontoCalculado { get; set; }

        [StringLength(30)]
        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [ForeignKey("IdEspacio")]
        public eEspacio Espacio { get; set; }

        [ForeignKey("IdVehiculo")]
        public eVehiculo Vehiculo { get; set; }

        [ForeignKey("IdCliente")]
        public eCliente Cliente { get; set; }

        [ForeignKey("IdTarifa")]
        public eTarifa Tarifa { get; set; }

        [ForeignKey("IdUsuario")]
        public eUsuario Usuario { get; set; }
    }
}
