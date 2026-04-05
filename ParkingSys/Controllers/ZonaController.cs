using Entity.Estructura;
using Factory;
using System.Web.Mvc;

namespace AppG2.Controllers
{
    public class ZonaController : Controller
    {
        fZona factory { get; set; }

        public ZonaController()
        {
            factory = new fZona();
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
            return View(new eZona());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(eZona entity)
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

            ViewBag.IdSucursal = new SelectList(factory.toListSucursal(), "Id", "Nombre", entity.IdSucursal);
            return View(entity);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            eZona entity = factory.getForId(id.Value);

            if (entity == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.IdSucursal = new SelectList(factory.toListSucursal(), "Id", "Nombre", entity.IdSucursal);
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(eZona entity)
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

            ViewBag.IdSucursal = new SelectList(factory.toListSucursal(), "Id", "Nombre", entity.IdSucursal);
            return View(entity);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            eZona entity = factory.getForId(id.Value);

            if (entity == null)
            {
                return RedirectToAction("Index");
            }

            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(eZona entity)
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