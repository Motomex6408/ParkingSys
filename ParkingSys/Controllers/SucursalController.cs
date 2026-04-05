using Entity.Estructura;
using Factory;
using System.Web.Mvc;

namespace AppG2.Controllers
{
    public class SucursalController : Controller
    {
        fSucursal factory { get; set; }

        public SucursalController()
        {
            factory = new fSucursal();
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
            return View(new eSucursal());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(eSucursal entity)
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

            return View(entity);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            eSucursal entity = factory.getForId(id.Value);

            if (entity == null)
            {
                return RedirectToAction("Index");
            }

            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(eSucursal entity)
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

            return View(entity);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            eSucursal entity = factory.getForId(id.Value);

            if (entity == null)
            {
                return RedirectToAction("Index");
            }

            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(eSucursal entity)
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