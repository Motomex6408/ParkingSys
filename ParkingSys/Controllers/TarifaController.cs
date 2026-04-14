using Entity.Administrador;
using Factory;
using System.Web.Mvc;

namespace AppG2.Controllers
{
    public class TarifaController : Controller
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

        fTarifa factory { get; set; }

        public TarifaController()
        {
            factory = new fTarifa();
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
            ViewBag.IdSucursal = new SelectList(factory.toListSucursal(), "Id", "Nombre");
            ViewBag.IdTipoVehiculo = new SelectList(factory.toListTipoVehiculo(), "Id", "Nombre");

            return View(new eTarifa());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(eTarifa entity)
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
                        auditoria.save(idUsuario, "TARIFA", "CREAR", "Se creó una nueva tarifa.");
                    }
                    return RedirectToAction("Index");
                }

                ViewBag.message = factory.message;
            }

            ViewBag.IdSucursal = new SelectList(factory.toListSucursal(), "Id", "Nombre", entity.IdSucursal);
            ViewBag.IdTipoVehiculo = new SelectList(factory.toListTipoVehiculo(), "Id", "Nombre", entity.IdTipoVehiculo);

            return View(entity);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            eTarifa entity = factory.getForId(id.Value);

            if (entity == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.IdSucursal = new SelectList(factory.toListSucursal(), "Id", "Nombre", entity.IdSucursal);
            ViewBag.IdTipoVehiculo = new SelectList(factory.toListTipoVehiculo(), "Id", "Nombre", entity.IdTipoVehiculo);

            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(eTarifa entity)
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
                        auditoria.save(idUsuario, "TARIFA", "EDITAR", "Se edito una tarifa.");
                    }
                    return RedirectToAction("Index");
                }

                ViewBag.message = factory.message;
            }

            ViewBag.IdSucursal = new SelectList(factory.toListSucursal(), "Id", "Nombre", entity.IdSucursal);
            ViewBag.IdTipoVehiculo = new SelectList(factory.toListTipoVehiculo(), "Id", "Nombre", entity.IdTipoVehiculo);

            return View(entity);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            eTarifa entity = factory.getForId(id.Value);

            if (entity == null)
            {
                return RedirectToAction("Index");
            }

            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(eTarifa entity)
        {
            factory.delete(entity.Id);

            if (factory.success)
            {
                if (Session["IdUsuario"] != null)
                {
                    int idUsuario = int.Parse(Session["IdUsuario"].ToString());
                    fAuditoria auditoria = new fAuditoria();
                    auditoria.save(idUsuario, "TARIFA", "ELIMINAR", "Se elimino una tarifa.");
                }
                return RedirectToAction("Index");
            }

            ViewBag.message = factory.message;
            return View(entity);
        }
    }
}