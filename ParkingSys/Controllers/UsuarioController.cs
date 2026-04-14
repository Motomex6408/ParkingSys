using Entity.Administrador;
using Factory;
using System.IO;
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
            if (!ModelState.IsValid)
            {
                string errores = "";

                foreach (var item in ModelState)
                {
                    foreach (var error in item.Value.Errors)
                    {
                        errores += item.Key + " => " + error.ErrorMessage + " | ";
                    }
                }

                ViewBag.message = errores;
            }

            if (ModelState.IsValid)
            {
                string carpeta = Server.MapPath("~/Content/uploads/usuarios/");

                if (!System.IO.Directory.Exists(carpeta))
                {
                    System.IO.Directory.CreateDirectory(carpeta);
                }

                if (fileFoto != null && fileFoto.ContentLength > 0)
                {
                    string nombreArchivo = System.IO.Path.GetFileName(fileFoto.FileName);
                    string ruta = System.IO.Path.Combine(carpeta, nombreArchivo);

                    fileFoto.SaveAs(ruta);
                    entity.Foto = nombreArchivo;
                }
                else
                {
                    entity.Foto = "default.png";
                }

                factory.save(entity);

                if (factory.success)
                {
                    if (Session["IdUsuario"] != null)
                    {
                        int idUsuario = int.Parse(Session["IdUsuario"].ToString());
                        fAuditoria auditoria = new fAuditoria();
                        auditoria.save(idUsuario, "USUARIO", "CREAR", "Se creó un nuevo usuario.");
                    }
                    return RedirectToAction("Index");
                }

                ViewBag.message = factory.message;
            }

            ViewBag.IdRol = new SelectList(factory.toListRol(), "Id", "Nombre", entity.IdRol);
            ViewBag.IdSucursal = new SelectList(factory.toListSucursal(), "Id", "Nombre", entity.IdSucursal);

            return View(entity);
        }

        public ActionResult Edit(int? id)
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

            // Para no enviar el hash a la vista
            entity.Password = "";

            ViewBag.IdRol = new SelectList(factory.toListRol(), "Id", "Nombre", entity.IdRol);
            ViewBag.IdSucursal = new SelectList(factory.toListSucursal(), "Id", "Nombre", entity.IdSucursal);

            return View(entity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(eUsuario entity, HttpPostedFileBase fileFoto)
        {
            if (ModelState.IsValid)
            {
                string carpeta = Server.MapPath("~/Content/uploads/usuarios/");

                if (!Directory.Exists(carpeta))
                {
                    Directory.CreateDirectory(carpeta);
                }

                if (fileFoto != null && fileFoto.ContentLength > 0)
                {
                    string nombreArchivo = Path.GetFileName(fileFoto.FileName);
                    string ruta = Path.Combine(carpeta, nombreArchivo);

                    fileFoto.SaveAs(ruta);
                    entity.Foto = nombreArchivo;
                }

                factory.save(entity);

                if (factory.success)
                {
                    if (Session["IdUsuario"] != null)
                    {
                        int idUsuario = int.Parse(Session["IdUsuario"].ToString());
                        fAuditoria auditoria = new fAuditoria();
                        auditoria.save(idUsuario, "USUARIO", "CREAR", "Se creó un nuevo usuario.");
                    }
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
                if (Session["IdUsuario"] != null)
                {
                    int idUsuario = int.Parse(Session["IdUsuario"].ToString());
                    fAuditoria auditoria = new fAuditoria();
                    auditoria.save(idUsuario, "USUARIO", "ELIMINAR", "Se elimino un usuario.");
                }
                return RedirectToAction("Index");
            }

            ViewBag.message = factory.message;
            return View(entity);
        }
    }
}