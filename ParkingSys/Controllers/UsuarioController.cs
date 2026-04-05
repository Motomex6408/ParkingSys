using Entity.Administrador;
using Factory;
using System.Web;
using System.Web.Mvc;

namespace AppG2.Controllers
{
    public class UsuarioController : Controller
    {
        fUsuario factory { get; set; }

        public UsuarioController()
        {
            factory = new fUsuario();
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
            ViewBag.IdRol = new SelectList(factory.toListRol(), "Id", "Nombre");
            ViewBag.IdSucursal = new SelectList(factory.toListSucursal(), "Id", "Nombre");

            return View(new eUsuario());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(eUsuario entity, HttpPostedFileBase fileFoto)
        {
            if (ModelState.IsValid)
            {
                if (fileFoto != null && fileFoto.ContentLength > 0)
                {
                    string nombreArchivo = System.IO.Path.GetFileName(fileFoto.FileName);
                    string ruta = Server.MapPath("~/Content/uploads/usuarios/" + nombreArchivo);

                    fileFoto.SaveAs(ruta);

                    entity.Foto = nombreArchivo;
                }

                factory.save(entity);

                if (factory.success)
                {
                    return RedirectToAction("Index");
                }

                ViewBag.message = factory.message;
            }

            ViewBag.IdRol = new SelectList(factory.toListRol(), "Id", "Nombre", entity.IdRol);
            ViewBag.IdSucursal = new SelectList(factory.toListSucursal(), "Id", "Nombre", entity.IdSucursal);

            return View(entity);
        }

        public ActionResult Edit(eUsuario entity, HttpPostedFileBase fileFoto)
        {
            if (ModelState.IsValid)
            {
                if (fileFoto != null && fileFoto.ContentLength > 0)
                {
                    string nombreArchivo = System.IO.Path.GetFileName(fileFoto.FileName);
                    string ruta = Server.MapPath("~/Content/uploads/usuarios/" + nombreArchivo);

                    fileFoto.SaveAs(ruta);

                    entity.Foto = nombreArchivo;
                }

                factory.save(entity);

                if (factory.success)
                {
                    return RedirectToAction("Index");
                }

                ViewBag.message = factory.message;
            }

            ViewBag.IdRol = new SelectList(factory.toListRol(), "Id", "Nombre", entity.IdRol);
            ViewBag.IdSucursal = new SelectList(factory.toListSucursal(), "Id", "Nombre", entity.IdSucursal);

            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(eUsuario entity)
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

            ViewBag.IdRol = new SelectList(factory.toListRol(), "Id", "Nombre", entity.IdRol);
            ViewBag.IdSucursal = new SelectList(factory.toListSucursal(), "Id", "Nombre", entity.IdSucursal);

            return View(entity);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            eUsuario entity = factory.getForId(id.Value);

            if (entity == null)
            {
                return RedirectToAction("Index");
            }

            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(eUsuario entity)
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