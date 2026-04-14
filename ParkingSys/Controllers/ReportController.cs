using CrystalDecisions.CrystalReports.Engine;
using DAO;
using Entity;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace ParkingSys.Controllers
{
    public class ReportController : Controller
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

        DbContextG2 db { get; set; }

        public ReportController()
        {
            db = new DbContextG2();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult User()
        {
            ReportDocument rpt = new ReportDocument();

            string path = Server.MapPath("~/Reports/rptUser.rpt");

            if (!System.IO.File.Exists(path))
            {
                return Content("No existe el archivo: " + path);
            }

            rpt.Load(path);

            string query = "Select * from Usuario";
            var getUsers = db.Database.SqlQuery<Models.Users>(query).ToList();

            AConvert conv = new AConvert();
            DataTable dt = conv.dataTable(getUsers);

            rpt.SetDataSource(dt);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Stream stream = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);

            rpt.Close();
            rpt.Dispose();

            return File(stream, "application/pdf");
        }
    }
}