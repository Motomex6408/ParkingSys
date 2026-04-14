using Entity.Administrador;
using Factory;
using System.Web.Mvc;

namespace AppG2.Controllers
{
    public class RolController : Controller
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

        fRol factory { get; set; }

        public RolController()
        {
            factory = new fRol();
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
            return View(new eRol());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(eRol entity)
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
                        auditoria.save(idUsuario, "ROL", "CREAR", "Se creó un nuevo rol.");
                    }
                    return RedirectToAction("Index");
                }

                ViewBag.message = factory.message;
            }

            return View(entity);
        }

        public ActionResult Edit(int id)
        {
            eRol entity = factory.getForId(id);
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(eRol entity)
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
                        auditoria.save(idUsuario, "ROL", "EDITAR", "Se edito un nuevo rol.");
                    }
                    return RedirectToAction("Index");
                }

                ViewBag.message = factory.message;
            }

            return View(entity);
        }

        public ActionResult Delete(int id)
        {
            eRol entity = factory.getForId(id);
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(eRol entity)
        {
            factory.delete(entity.Id);

            if (factory.success)
            {
                if (Session["IdUsuario"] != null)
                {
                    int idUsuario = int.Parse(Session["IdUsuario"].ToString());
                    fAuditoria auditoria = new fAuditoria();
                    auditoria.save(idUsuario, "ROL", "ELIMINAR", "Se elimino un nuevo rol.");
                }
                return RedirectToAction("Index");
            }

            ViewBag.message = factory.message;
            return View(entity);
        }
    }
}