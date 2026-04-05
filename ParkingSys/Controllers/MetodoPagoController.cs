using Entity.Facturacion;
using Factory;
using System.Web.Mvc;

namespace AppG2.Controllers
{
    public class MetodoPagoController : Controller
    {
        fMetodoPago factory { get; set; }

        public MetodoPagoController()
        {
            factory = new fMetodoPago();
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
            return View(new eMetodoPago());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(eMetodoPago entity)
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

        public ActionResult Edit(int id)
        {
            eMetodoPago entity = factory.getForId(id);
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(eMetodoPago entity)
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

        public ActionResult Delete(int id)
        {
            eMetodoPago entity = factory.getForId(id);
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(eMetodoPago entity)
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