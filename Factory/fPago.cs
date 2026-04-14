using DAO;
using Entity.Facturacion;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Factory
{
    public class fPago
    {
        DbContextG2 db { get; set; }

        public bool success { get; set; }
        public string message { get; set; }

        public fPago()
        {
            db = new DbContextG2();
            success = false;
            message = "";
        }

        public List<ePago> toList(string text)
        {
            if (text == null)
            {
                text = "";
            }

            List<ePago> list = db.Pago
                .Include("Factura")
                .Include("MetodoPago")
                .Where(x => x.Estado.Contains(text)
                         || (x.Referencia != null && x.Referencia.Contains(text))
                         || (x.Factura != null && x.Factura.Folio.Contains(text))
                         || (x.MetodoPago != null && x.MetodoPago.Nombre.Contains(text)))
                .ToList();

            return list;
        }

        public ePago getForId(int id)
        {
            ePago entity = db.Pago
                .Include("Factura")
                .Include("MetodoPago")
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return entity;
        }

        public void save(ePago entity)
        {
            try
            {
                eFactura factura = db.Factura
                    .Include("Ticket")
                    .Where(x => x.Id == entity.IdFactura)
                    .FirstOrDefault();

                if (factura == null)
                {
                    success = false;
                    message = "La factura seleccionada no existe.";
                    return;
                }

                if (factura.Ticket == null || factura.Ticket.Estado != "CERRADO")
                {
                    success = false;
                    message = "Solo se puede registrar pago de una factura asociada a un ticket cerrado.";
                    return;
                }

                eMetodoPago metodo = db.MetodoPago
                    .Where(x => x.Id == entity.IdMetodoPago && x.Activo == true)
                    .FirstOrDefault();

                if (metodo == null)
                {
                    success = false;
                    message = "El método de pago no es válido.";
                    return;
                }

                bool yaPagada = db.Pago.Any(x => x.IdFactura == entity.IdFactura && x.Estado == "PAGADO");

                if (yaPagada)
                {
                    success = false;
                    message = "Esa factura ya fue pagada.";
                    return;
                }

                entity.Monto = factura.Total;
                entity.FechaPago = DateTime.Now;
                entity.Estado = "PAGADO";

                db.Pago.Add(entity);
                db.SaveChanges();

                success = true;
                message = "Pago registrado correctamente.";
            }
            catch (Exception ex)
            {
                success = false;
                message = ex.Message;
            }
        }

        public void delete(int id)
        {
            try
            {
                ePago entity = db.Pago
                    .Where(x => x.Id == id)
                    .FirstOrDefault();

                if (entity != null)
                {
                    db.Pago.Remove(entity);
                    db.SaveChanges();
                    success = true;
                    message = "Eliminado correctamente";
                }
                else
                {
                    success = false;
                    message = "No existe";
                }
            }
            catch (Exception ex)
            {
                success = false;
                message = ex.Message;
            }
        }

        public List<eFactura> toListFactura()
        {
            return db.Factura
                .Include("Ticket")
                .Where(x => x.Estado == "EMITIDA")
                .ToList();
        }

        public List<eMetodoPago> toListMetodoPago()
        {
            return db.MetodoPago
                .Where(x => x.Activo == true)
                .ToList();
        }
    }
}