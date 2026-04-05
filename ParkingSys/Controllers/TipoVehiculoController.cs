using Entity.General;
using Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppG2.Controllers
{
    public class TipoVehiculoController : Controller
    {
        fTipoVehiculo factory { get; set; }

        public TipoVehiculoController()
        {
            factory = new fTipoVehiculo();
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
            return View(new eTipoVehiculo());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(eTipoVehiculo entity)
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
            eTipoVehiculo entity = factory.getForId(id);
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(eTipoVehiculo entity)
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
            eTipoVehiculo entity = factory.getForId(id);
            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(eTipoVehiculo entity)
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