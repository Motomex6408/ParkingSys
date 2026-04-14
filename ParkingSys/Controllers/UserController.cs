using Entity;
using Entity.Administrador;
using Factory;
using System.Web.Mvc;

namespace AppG2.Controllers
{
    public class UserController : Controller
    {
      
        fUsuario factory { get; set; }

        public UserController()
        {
            factory = new fUsuario();
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            eLogin entity = new eLogin();
            entity.toURL = returnUrl;

            return View(entity);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(eLogin entity)
        {
            if (ModelState.IsValid)
            {
                eUsuario user = factory.login(entity.Access, entity.Password);

                if (user != null)
                {
                    Session["IdUsuario"] = user.Id;
                    Session["Nombre"] = user.Nombre + " " + user.Apellido;
                    Session["UserName"] = user.Nombre;
                    Session["Rol"] = user.Rol != null ? user.Rol.Nombre : "";
                    Session["Foto"] = string.IsNullOrEmpty(user.Foto) ? "default.png" : user.Foto;
                    Session["Email"] = user.Email;

                    fAuditoria auditoria = new fAuditoria();
                    auditoria.save(user.Id, "LOGIN", "INICIO DE SESION", "El usuario inició sesión en el sistema.");

                    if (!string.IsNullOrEmpty(entity.toURL))
                    {
                        return Redirect(entity.toURL);
                    }

                    return RedirectToAction("Index", "Home");
                }

                ViewBag.message = "Usuario/correo o clave incorrectos.";
            }

            return View(entity);
        }


        public ActionResult Logout()
        {
            if (Session["IdUsuario"] != null)
            {
                int idUsuario = int.Parse(Session["IdUsuario"].ToString());
                fAuditoria auditoria = new fAuditoria();
                auditoria.save(idUsuario, "LOGIN", "CERRAR SESION", "El usuario cerró sesión en el sistema.");
            }

            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login", "User");
        }
    }
}