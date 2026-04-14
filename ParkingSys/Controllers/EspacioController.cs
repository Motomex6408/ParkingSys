using Entity.Estructura;
using Factory;
using System.Web.Mvc;

namespace AppG2.Controllers
{
    public class EspacioController : Controller
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

        fEspacio factory { get; set; }

        public EspacioController()
        {
            factory = new fEspacio();
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
            ViewBag.IdZona = new SelectList(factory.toListZona(), "Id", "Nombre");
            ViewBag.IdTipoVehiculo = new SelectList(factory.toListTipoVehiculo(), "Id", "Nombre");

            eEspacio entity = new eEspacio();
            entity.Disponible = true;

            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(eEspacio entity)
        {
            if (ModelState.IsValid)
            {
                factory.save(entity);

                if (factory.success)
                {
                    return RedirectToAction("Index");
                }

                ViewBag.message = factory.message;
            }

            ViewBag.IdZona = new SelectList(factory.toListZona(), "Id", "Nombre", entity.IdZona);
            ViewBag.IdTipoVehiculo = new SelectList(factory.toListTipoVehiculo(), "Id", "Nombre", entity.IdTipoVehiculo);

            return View(entity);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            eEspacio entity = factory.getForId(id.Value);

            if (entity == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.IdZona = new SelectList(factory.toListZona(), "Id", "Nombre", entity.IdZona);
            ViewBag.IdTipoVehiculo = new SelectList(factory.toListTipoVehiculo(), "Id", "Nombre", entity.IdTipoVehiculo);

            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(eEspacio entity)
        {
            if (ModelState.IsValid)
            {
                factory.save(entity);

                if (factory.success)
                {
                    return RedirectToAction("Index");
                }

                ViewBag.message = factory.message;
            }

            ViewBag.IdZona = new SelectList(factory.toListZona(), "Id", "Nombre", entity.IdZona);
            ViewBag.IdTipoVehiculo = new SelectList(factory.toListTipoVehiculo(), "Id", "Nombre", entity.IdTipoVehiculo);

            return View(entity);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            eEspacio entity = factory.getForId(id.Value);

            if (entity == null)
            {
                return RedirectToAction("Index");
            }

            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(eEspacio entity)
        {
            factory.delete(entity.Id);

            if (factory.success)
            {
                return RedirectToAction("Index");
            }

            ViewBag.message = factory.message;
            return View(entity);
        }
    }
}