using DAO;
using System;
using System.Data.Entity;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace ParkingSys.Controllers
{
    public class ReportController : Controller
    {
        DbContextG2 db { get; set; }

        public ReportController()
        {
            db = new DbContextG2();
        }

        public ActionResult FacturasPdf()
        {
            var data = db.Factura
                .Include("Cliente")
                .OrderByDescending(x => x.FechaEmision)
                .ToList();

            decimal totalFacturado = data.Sum(x => x.Total);

            PdfDocument document = new PdfDocument();
            document.Info.Title = "Reporte de Facturas - ParkingSys";

            PdfPage page = document.AddPage();
            page.Size = PdfSharp.PageSize.A4;

            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont fontTitle = new XFont("Arial", 16, XFontStyle.Bold);
            XFont fontSubTitle = new XFont("Arial", 10, XFontStyle.Regular);
            XFont fontHeader = new XFont("Arial", 9, XFontStyle.Bold);
            XFont fontBody = new XFont("Arial", 8, XFontStyle.Regular);
            XFont fontTotal = new XFont("Arial", 11, XFontStyle.Bold);

            double marginLeft = 40;
            double y = 40;

            // Logo
            string logoPath = Server.MapPath("~/assets/images/logo.png");
            if (System.IO.File.Exists(logoPath))
            {
                XImage logo = XImage.FromFile(logoPath);
                gfx.DrawImage(logo, marginLeft, y, 60, 60);
            }

            // Título
            gfx.DrawString("ParkingSys", fontTitle, XBrushes.Black,
                new XRect(0, y, page.Width, 20), XStringFormats.TopCenter);

            gfx.DrawString("Reporte de Facturas", fontSubTitle, XBrushes.Gray,
                new XRect(0, y + 22, page.Width, 20), XStringFormats.TopCenter);

            gfx.DrawString("Fecha de generación: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                fontSubTitle, XBrushes.Gray,
                new XRect(0, y + 38, page.Width, 20), XStringFormats.TopCenter);

            y += 90;

            // Columnas
            double[] widths = { 70, 120, 70, 70, 60, 70, 70 };
            string[] headers = { "Folio", "Cliente", "Fecha", "Subtotal", "ISV", "Total", "Estado" };

            double posX = marginLeft;

            // Header row
            for (int i = 0; i < headers.Length; i++)
            {
                gfx.DrawRectangle(XBrushes.DarkSlateGray, posX, y, widths[i], 22);
                gfx.DrawString(headers[i], fontHeader, XBrushes.White,
                    new XRect(posX, y + 4, widths[i], 20), XStringFormats.TopCenter);
                posX += widths[i];
            }

            y += 22;

            // Rows
            foreach (var item in data)
            {
                if (y > page.Height - 60)
                {
                    page = document.AddPage();
                    page.Size = PdfSharp.PageSize.A4;
                    gfx = XGraphics.FromPdfPage(page);
                    y = 40;

                    posX = marginLeft;
                    for (int i = 0; i < headers.Length; i++)
                    {
                        gfx.DrawRectangle(XBrushes.DarkSlateGray, posX, y, widths[i], 22);
                        gfx.DrawString(headers[i], fontHeader, XBrushes.White,
                            new XRect(posX, y + 4, widths[i], 20), XStringFormats.TopCenter);
                        posX += widths[i];
                    }
                    y += 22;
                }

                string cliente = item.Cliente != null
                    ? item.Cliente.Nombre + " " + item.Cliente.Apellido
                    : "";

                string[] row =
                {
            item.Folio,
            cliente,
            item.FechaEmision.ToString("dd/MM/yyyy"),
            "L " + item.Subtotal.ToString("N2"),
            "L " + item.Impuesto.ToString("N2"),
            "L " + item.Total.ToString("N2"),
            item.Estado
        };

                posX = marginLeft;

                for (int i = 0; i < row.Length; i++)
                {
                    gfx.DrawRectangle(XPens.LightGray, posX, y, widths[i], 20);
                    gfx.DrawString(row[i], fontBody, XBrushes.Black,
                        new XRect(posX + 2, y + 4, widths[i] - 4, 20), XStringFormats.TopLeft);
                    posX += widths[i];
                }

                y += 20;
            }

            y += 20;

            gfx.DrawString("Total facturado: L " + totalFacturado.ToString("N2"),
                fontTotal, XBrushes.Black,
                new XRect(marginLeft, y, page.Width - marginLeft - 40, 20), XStringFormats.TopLeft);

            using (MemoryStream stream = new MemoryStream())
            {
                document.Save(stream, false);
                return File(stream.ToArray(), "application/pdf", "ReporteFacturas.pdf");
            }
        }

        public ActionResult IngresosPdf()
        {
            var data = db.Pago
                .Include("Factura")
                .Include("MetodoPago")
                .Where(x => x.Estado == "PAGADO")
                .OrderByDescending(x => x.FechaPago)
                .ToList();

            decimal totalIngresos = data.Sum(x => x.Monto);

            PdfDocument document = new PdfDocument();
            document.Info.Title = "Reporte de Ingresos - ParkingSys";

            PdfPage page = document.AddPage();
            page.Size = PdfSharp.PageSize.A4;

            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont fontTitle = new XFont("Arial", 16, XFontStyle.Bold);
            XFont fontSubTitle = new XFont("Arial", 10, XFontStyle.Regular);
            XFont fontHeader = new XFont("Arial", 9, XFontStyle.Bold);
            XFont fontBody = new XFont("Arial", 8, XFontStyle.Regular);
            XFont fontTotal = new XFont("Arial", 11, XFontStyle.Bold);

            double marginLeft = 40;
            double y = 40;

            
            string logoPath = Server.MapPath("~/assets/images/logo.png");
            if (System.IO.File.Exists(logoPath))
            {
                XImage logo = XImage.FromFile(logoPath);
                gfx.DrawImage(logo, marginLeft, y, 60, 60);
            }

            
            gfx.DrawString("ParkingSys", fontTitle, XBrushes.Black,
                new XRect(0, y, page.Width, 20), XStringFormats.TopCenter);

            gfx.DrawString("Reporte de Ingresos", fontSubTitle, XBrushes.Gray,
                new XRect(0, y + 22, page.Width, 20), XStringFormats.TopCenter);

            gfx.DrawString("Fecha de generación: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                fontSubTitle, XBrushes.Gray,
                new XRect(0, y + 38, page.Width, 20), XStringFormats.TopCenter);

            y += 90;

           
            double[] widths = { 80, 100, 80, 100, 120, 80 };
            string[] headers = { "Factura", "Método Pago", "Monto", "Fecha", "Referencia", "Estado" };

            double posX = marginLeft;

            
            for (int i = 0; i < headers.Length; i++)
            {
                gfx.DrawRectangle(XBrushes.DarkSlateGray, posX, y, widths[i], 22);
                gfx.DrawString(headers[i], fontHeader, XBrushes.White,
                    new XRect(posX, y + 4, widths[i], 20), XStringFormats.TopCenter);
                posX += widths[i];
            }

            y += 22;

            
            foreach (var item in data)
            {
                if (y > page.Height - 60)
                {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    y = 40;

                    posX = marginLeft;
                    for (int i = 0; i < headers.Length; i++)
                    {
                        gfx.DrawRectangle(XBrushes.DarkSlateGray, posX, y, widths[i], 22);
                        gfx.DrawString(headers[i], fontHeader, XBrushes.White,
                            new XRect(posX, y + 4, widths[i], 20), XStringFormats.TopCenter);
                        posX += widths[i];
                    }

                    y += 22;
                }

                string[] row =
                {
            item.Factura != null ? item.Factura.Folio : "",
            item.MetodoPago != null ? item.MetodoPago.Nombre : "",
            "L " + item.Monto.ToString("N2"),
            item.FechaPago.ToString("dd/MM/yyyy"),
            item.Referencia,
            item.Estado
        };

                posX = marginLeft;

                for (int i = 0; i < row.Length; i++)
                {
                    gfx.DrawRectangle(XPens.LightGray, posX, y, widths[i], 20);
                    gfx.DrawString(row[i], fontBody, XBrushes.Black,
                        new XRect(posX + 2, y + 4, widths[i] - 4, 20), XStringFormats.TopLeft);
                    posX += widths[i];
                }

                y += 20;
            }

            
            y += 20;

            var formatRight = new XStringFormat();
            formatRight.Alignment = XStringAlignment.Far;
            formatRight.LineAlignment = XLineAlignment.Near;

            gfx.DrawString("Total ingresos: L " + totalIngresos.ToString("N2"),
                fontTotal, XBrushes.Black,
                new XRect(marginLeft, y, page.Width - marginLeft - 40, 20),
                formatRight);

            using (MemoryStream stream = new MemoryStream())
            {
                document.Save(stream, false);
                return File(stream.ToArray(), "application/pdf", "ReporteIngresos.pdf");
            }
        }

        public ActionResult TicketsPdf()
        {
            var data = db.Ticket
                .Include("Vehiculo")
                .Include("Cliente")
                .Include("Espacio")
                .OrderByDescending(x => x.Entrada)
                .ToList();

            PdfDocument document = new PdfDocument();
            document.Info.Title = "Reporte de Tickets - ParkingSys";

            PdfPage page = document.AddPage();
            page.Size = PdfSharp.PageSize.A4;

            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont fontTitle = new XFont("Arial", 16, XFontStyle.Bold);
            XFont fontSubTitle = new XFont("Arial", 10, XFontStyle.Regular);
            XFont fontHeader = new XFont("Arial", 9, XFontStyle.Bold);
            XFont fontBody = new XFont("Arial", 8, XFontStyle.Regular);

            double marginLeft = 40;
            double y = 40;

           
            string logoPath = Server.MapPath("~/assets/images/logo.png");
            if (System.IO.File.Exists(logoPath))
            {
                XImage logo = XImage.FromFile(logoPath);
                gfx.DrawImage(logo, marginLeft, y, 60, 60);
            }

            
            gfx.DrawString("ParkingSys", fontTitle, XBrushes.Black,
                new XRect(0, y, page.Width, 20), XStringFormats.TopCenter);

            gfx.DrawString("Reporte de Tickets", fontSubTitle, XBrushes.Gray,
                new XRect(0, y + 22, page.Width, 20), XStringFormats.TopCenter);

            gfx.DrawString("Fecha de generación: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                fontSubTitle, XBrushes.Gray,
                new XRect(0, y + 38, page.Width, 20), XStringFormats.TopCenter);

            y += 90;

            
            double[] widths = { 70, 110, 70, 90, 90, 70, 80, 70 };
            string[] headers = { "Placa", "Cliente", "Espacio", "Entrada", "Salida", "Minutos", "Monto", "Estado" };

            double posX = marginLeft;

            
            for (int i = 0; i < headers.Length; i++)
            {
                gfx.DrawRectangle(XBrushes.DarkSlateGray, posX, y, widths[i], 22);
                gfx.DrawString(headers[i], fontHeader, XBrushes.White,
                    new XRect(posX, y + 4, widths[i], 20), XStringFormats.TopCenter);
                posX += widths[i];
            }

            y += 22;

           
            foreach (var item in data)
            {
                if (y > page.Height - 60)
                {
                    page = document.AddPage();
                    gfx = XGraphics.FromPdfPage(page);
                    y = 40;

                    posX = marginLeft;
                    for (int i = 0; i < headers.Length; i++)
                    {
                        gfx.DrawRectangle(XBrushes.DarkSlateGray, posX, y, widths[i], 22);
                        gfx.DrawString(headers[i], fontHeader, XBrushes.White,
                            new XRect(posX, y + 4, widths[i], 20), XStringFormats.TopCenter);
                        posX += widths[i];
                    }

                    y += 22;
                }

                string cliente = item.Cliente != null
                    ? item.Cliente.Nombre + " " + item.Cliente.Apellido
                    : "";

                string[] row =
                {
            item.Vehiculo != null ? item.Vehiculo.Placa : "",
            cliente,
            item.Espacio != null ? item.Espacio.Codigo : "",
            item.Entrada.ToString("dd/MM/yyyy HH:mm"),
            item.Salida.HasValue ? item.Salida.Value.ToString("dd/MM/yyyy HH:mm") : "",
            item.MinutosTotales.ToString(),
            "L " + item.MontoCalculado.ToString("N2"),
            item.Estado
        };

                posX = marginLeft;

                for (int i = 0; i < row.Length; i++)
                {
                    gfx.DrawRectangle(XPens.LightGray, posX, y, widths[i], 20);
                    gfx.DrawString(row[i], fontBody, XBrushes.Black,
                        new XRect(posX + 2, y + 4, widths[i] - 4, 20), XStringFormats.TopLeft);
                    posX += widths[i];
                }

                y += 20;
            }

            using (MemoryStream stream = new MemoryStream())
            {
                document.Save(stream, false);
                return File(stream.ToArray(), "application/pdf", "ReporteTickets.pdf");
            }
        }

        public ActionResult AuditoriaPdf()
        {
            var data = db.Auditoria
                .Include("Usuario")
                .OrderByDescending(x => x.Fecha)
                .ToList();

            PdfDocument document = new PdfDocument();
            document.Info.Title = "Reporte de Auditoría - ParkingSys";

            PdfPage page = document.AddPage();
            page.Size = PdfSharp.PageSize.A4;
            page.Orientation = PdfSharp.PageOrientation.Landscape;

            XGraphics gfx = XGraphics.FromPdfPage(page);

            XFont fontTitle = new XFont("Arial", 16, XFontStyle.Bold);
            XFont fontSubTitle = new XFont("Arial", 10, XFontStyle.Regular);
            XFont fontHeader = new XFont("Arial", 9, XFontStyle.Bold);
            XFont fontBody = new XFont("Arial", 8, XFontStyle.Regular);

            double marginLeft = 30;
            double y = 30;

            // Logo
            string logoPath = Server.MapPath("~/assets/images/logo.png");
            if (System.IO.File.Exists(logoPath))
            {
                XImage logo = XImage.FromFile(logoPath);
                gfx.DrawImage(logo, marginLeft, y, 60, 60);
            }

            // Títulos
            gfx.DrawString("ParkingSys", fontTitle, XBrushes.Black,
                new XRect(0, y, page.Width, 20), XStringFormats.TopCenter);

            gfx.DrawString("Reporte de Auditoría", fontSubTitle, XBrushes.Gray,
                new XRect(0, y + 22, page.Width, 20), XStringFormats.TopCenter);

            gfx.DrawString("Fecha de generación: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"),
                fontSubTitle, XBrushes.Gray,
                new XRect(0, y + 38, page.Width, 20), XStringFormats.TopCenter);

            y += 90;

            // Columnas
            double[] widths = { 140, 90, 90, 260, 110 };
            string[] headers = { "Usuario", "Módulo", "Acción", "Descripción", "Fecha" };

            double posX = marginLeft;

            // Header
            for (int i = 0; i < headers.Length; i++)
            {
                gfx.DrawRectangle(XBrushes.DarkSlateGray, posX, y, widths[i], 22);
                gfx.DrawString(headers[i], fontHeader, XBrushes.White,
                    new XRect(posX, y + 4, widths[i], 20), XStringFormats.TopCenter);
                posX += widths[i];
            }

            y += 22;

            // Filas
            foreach (var item in data)
            {
                if (y > page.Height - 50)
                {
                    page = document.AddPage();
                    page.Size = PdfSharp.PageSize.A4;
                    page.Orientation = PdfSharp.PageOrientation.Landscape;
                    gfx = XGraphics.FromPdfPage(page);
                    y = 30;

                    posX = marginLeft;
                    for (int i = 0; i < headers.Length; i++)
                    {
                        gfx.DrawRectangle(XBrushes.DarkSlateGray, posX, y, widths[i], 22);
                        gfx.DrawString(headers[i], fontHeader, XBrushes.White,
                            new XRect(posX, y + 4, widths[i], 20), XStringFormats.TopCenter);
                        posX += widths[i];
                    }

                    y += 22;
                }

                string usuario = item.Usuario != null
                    ? item.Usuario.Nombre + " " + item.Usuario.Apellido
                    : "";

                string[] row =
                {
            usuario,
            item.Modulo,
            item.Accion,
            item.Descripcion,
            item.Fecha.ToString("dd/MM/yyyy HH:mm")
        };

                posX = marginLeft;

                for (int i = 0; i < row.Length; i++)
                {
                    gfx.DrawRectangle(XPens.LightGray, posX, y, widths[i], 22);
                    gfx.DrawString(row[i] ?? "", fontBody, XBrushes.Black,
                        new XRect(posX + 2, y + 5, widths[i] - 4, 20), XStringFormats.TopLeft);
                    posX += widths[i];
                }

                y += 22;
            }

            using (MemoryStream stream = new MemoryStream())
            {
                document.Save(stream, false);
                return File(stream.ToArray(), "application/pdf", "ReporteAuditoria.pdf");
            }
        }
    }
}