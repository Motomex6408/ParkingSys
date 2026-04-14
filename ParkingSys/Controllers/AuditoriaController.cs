using Factory;
using System.Web.Mvc;

namespace AppG2.Controllers
{
    public class AuditoriaController : Controller
    {
        fAuditoria factory { get; set; }

        public AuditoriaController()
        {
            factory = new fAuditoria();
        }

        public ActionResult Index(string id)
        {
            if (Session["IdUsuario"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            if (Session["Rol"] == null || Session["Rol"].ToString() != "Administrador")
            {
                return RedirectToAction("Index", "Home");
            }

            if (id == null)
            {
                id = "";
            }

            return View(factory.toList(id));
        }
    }
}