using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingSys.Models
{
        public class Ingresos
        {
            public int Id { get; set; }
            public string Folio { get; set; }
            public decimal Monto { get; set; }
            public DateTime FechaPago { get; set; }
            public string Referencia { get; set; }
            public string Estado { get; set; }
            public string MetodoPago { get; set; }
        }
    
}