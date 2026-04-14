using Entity.General;
using Factory;
using System.Web.Mvc;

namespace AppG2.Controllers
{
    public class TicketController : Controller
    {

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["IdUsuario"] == null)
            {
                filterContext.Result = RedirectToAction("Login", "User");
                return;
            }

            base.OnActionExecuting(filterContext);
        }

        fTicket factory { get; set; }

        public TicketController()
        {
            factory = new fTicket();
        }

        public ActionResult Index(string id)
        {
            if (id == null)
            {
                id = "";
            }

            return View(factory.toList(id));
        }

        public ActionResult Create()
        {
            ViewBag.IdVehiculo = new SelectList(factory.toListVehiculo(), "Id", "Placa");
            ViewBag.IdCliente = new SelectList(factory.toListCliente(), "Id", "Nombre");
            ViewBag.IdEspacio = new SelectList(factory.toListEspacio(), "Id", "Codigo");
            ViewBag.IdUsuario = new SelectList(factory.toListUsuario(), "Id", "Nombre");
            ViewBag.IdTarifa = new SelectList(factory.toListTarifa(), "Id", "Nombre");

            return View(new eTicket());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(eTicket entity)
        {
            if (ModelState.IsValid)
            {
                factory.save(entity);

                if (factory.success)
                {
                    if (Session["IdUsuario"] != null)
                    {
                        int idUsuario = int.Parse(Session["IdUsuario"].ToString());
                        fAuditoria auditoria = new fAuditoria();
                        auditoria.save(idUsuario, "TICKET", "CREAR", "Se registró un ticket de entrada.");
                    }
                    return RedirectToAction("Index");
                }

                ViewBag.message = factory.message;
            }

            ViewBag.IdVehiculo = new SelectList(factory.toListVehiculo(), "Id", "Placa", entity.IdVehiculo);
            ViewBag.IdCliente = new SelectList(factory.toListCliente(), "Id", "Nombre", entity.IdCliente);
            ViewBag.IdEspacio = new SelectList(factory.toListEspacio(), "Id", "Codigo", entity.IdEspacio);
            ViewBag.IdUsuario = new SelectList(factory.toListUsuario(), "Id", "Nombre", entity.IdUsuario);
            ViewBag.IdTarifa = new SelectList(factory.toListTarifa(), "Id", "Nombre", entity.IdTarifa);

            return View(entity);
        }


        public ActionResult Close(int? id)
        {
            if (id == null)
            {           
                return RedirectToAction("Index");
            }

            eTicket entity = factory.getForId(id.Value);

            if (entity == null)
            {
                return RedirectToAction("Index");
            }

            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Close(eTicket entity)
        {
            factory.closeTicket(entity.Id);

            if (factory.success)
            {
                if (Session["IdUsuario"] != null)
                {
                    int idUsuario = int.Parse(Session["IdUsuario"].ToString());
                    fAuditoria auditoria = new fAuditoria();
                    auditoria.save(idUsuario, "TICKET", "CIERRE", "Se cerró un ticket de salida.");
                }
                return RedirectToAction("Index");
            }

            ViewBag.message = factory.message;

            eTicket obj = factory.getForId(entity.Id);
            return View(obj);
        }
    }
}