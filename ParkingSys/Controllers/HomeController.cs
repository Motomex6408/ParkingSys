using DAO;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;

namespace AppG2.Controllers
{
    public class HomeController : Controller
    {
        

        DbContextG2 db { get; set; }

        public HomeController()
        {
            db = new DbContextG2();
        }

        public ActionResult Index()
        {
            ViewBag.EspaciosDisponibles = db.Espacio.Count(x => x.Disponible == true);
            ViewBag.EspaciosOcupados = db.Espacio.Count(x => x.Disponible == false);
            ViewBag.TicketsActivos = db.Ticket.Count(x => x.Estado == "ACTIVO");

            DateTime hoy = DateTime.Today;
            DateTime manana = hoy.AddDays(1);

            ViewBag.IngresosHoy = db.Pago
                .Where(x => x.FechaPago >= hoy && x.FechaPago < manana && x.Estado == "PAGADO")
                .Select(x => (decimal?)x.Monto)
                .Sum() ?? 0;

            ViewBag.IngresosTotales = db.Pago
                .Where(x => x.Estado == "PAGADO")
                .Select(x => (decimal?)x.Monto)
                .Sum() ?? 0;



            ViewBag.TotalFacturas = db.Factura.Count();
            ViewBag.TotalPagos = db.Pago.Count();

            var tickets = db.Ticket
                .Include("Vehiculo")
                .Where(x => x.Estado == "ACTIVO")
                .OrderByDescending(x => x.Entrada)
                .Take(5)
                .ToList();

           

            var pagos = db.Pago
                .Include("Factura")
                .OrderByDescending(x => x.FechaPago)
                .Take(5)
                .ToList();

            var auditoria = db.Auditoria
                .Include("Usuario")
                .OrderByDescending(x => x.Fecha)
                .Take(5)
                .ToList();
            


            var usuarios = db.Usuario
                .Where(x => x.Activo == true)
                .OrderByDescending(x => x.Id)
                .Take(6)
                .ToList();

            ViewBag.Usuarios = usuarios;
            ViewBag.Tickets = tickets;
            ViewBag.Pagos = pagos;
            ViewBag.Auditoria = auditoria;

            return View();
        }
    }
}