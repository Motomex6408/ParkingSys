using Entity.General;
using Factory;
using System.Web.Mvc;

namespace AppG2.Controllers
{
    public class VehiculoController : Controller
    {
        fVehiculo factory { get; set; }

        public VehiculoController()
        {
            factory = new fVehiculo();
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
            ViewBag.IdTipoVehiculo = new SelectList(factory.toListTipoVehiculo(), "Id", "Nombre");
            return View(new eVehiculo());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(eVehiculo entity)
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

            ViewBag.IdTipoVehiculo = new SelectList(factory.toListTipoVehiculo(), "Id", "Nombre", entity.IdTipoVehiculo);
            return View(entity);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            eVehiculo entity = factory.getForId(id.Value);

            if (entity == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.IdTipoVehiculo = new SelectList(factory.toListTipoVehiculo(), "Id", "Nombre", entity.IdTipoVehiculo);
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(eVehiculo entity)
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

            ViewBag.IdTipoVehiculo = new SelectList(factory.toListTipoVehiculo(), "Id", "Nombre", entity.IdTipoVehiculo);
            return View(entity);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            eVehiculo entity = factory.getForId(id.Value);

            if (entity == null)
            {
                return RedirectToAction("Index");
            }

            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(eVehiculo entity)
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