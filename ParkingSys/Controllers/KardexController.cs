using DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParkingSys.Controllers
{
    public class KardexController : Controller
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

        DbContextG2 db = new DbContextG2();

        public ActionResult Index()
        {
            var list = db.Kardex
                .Include("Ticket.Vehiculo")
                .ToList();

            return View(list);
        }
    }
}