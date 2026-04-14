using Entity.Facturacion;
using Factory;
using System.Web.Mvc;

namespace AppG2.Controllers
{
    public class PagoController : Controller
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

        fPago factory { get; set; }

        public PagoController()
        {
            factory = new fPago();
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
            ViewBag.IdFactura = new SelectList(factory.toListFactura(), "Id", "Folio");
            ViewBag.IdMetodoPago = new SelectList(factory.toListMetodoPago(), "Id", "Nombre");

            return View(new ePago());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ePago entity)
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
                        auditoria.save(idUsuario, "PAGO", "CREAR", "Se registró un pago.");
                    }
                    return RedirectToAction("Index");
                }

                ViewBag.message = factory.message;
            }

            ViewBag.IdFactura = new SelectList(factory.toListFactura(), "Id", "Folio", entity.IdFactura);
            ViewBag.IdMetodoPago = new SelectList(factory.toListMetodoPago(), "Id", "Nombre", entity.IdMetodoPago);

            return View(entity);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            ePago entity = factory.getForId(id.Value);

            if (entity == null)
            {
                return RedirectToAction("Index");
            }

            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(ePago entity)
        {
            factory.delete(entity.Id);

            if (factory.success)
            {
                if (Session["IdUsuario"] != null)
                {
                    int idUsuario = int.Parse(Session["IdUsuario"].ToString());
                    fAuditoria auditoria = new fAuditoria();
                    auditoria.save(idUsuario, "PAGO", "ELIMINAR", "Se elimino un pago.");
                }
                return RedirectToAction("Index");
            }

            ViewBag.message = factory.message;
            return View(entity);
        }
    }
}