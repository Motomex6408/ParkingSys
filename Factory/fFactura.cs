using DAO;
using Entity.Facturacion;
using Entity.General;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Factory
{
    public class fFactura
    {
        DbContextG2 db { get; set; }

        public bool success { get; set; }
        public string message { get; set; }

        public fFactura()
        {
            db = new DbContextG2();
            success = false;
            message = "";
        }

        public List<eFactura> toList(string text)
        {
            if (text == null)
            {
                text = "";
            }

            List<eFactura> list = db.Factura
                .Include("Ticket")
                .Include("Cliente")
                .Where(x => x.Folio.Contains(text)
                         || (x.Serie != null && x.Serie.Contains(text))
                         || (x.Cliente != null && x.Cliente.Nombre.Contains(text))
                         || (x.Ticket != null && x.Ticket.Estado.Contains(text)))
                .ToList();

            return list;
        }

        public eFactura getForId(int id)
        {
            eFactura entity = db.Factura
                .Include("Ticket")
                .Include("Cliente")
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return entity;
        }

        public void save(eFactura entity)
        {
            try
            {
                eTicket ticket = db.Ticket
                    .Include("Cliente")
                    .Where(x => x.Id == entity.IdTicket)
                    .FirstOrDefault();

                if (ticket == null)
                {
                    success = false;
                    message = "El ticket seleccionado no existe.";
                    return;
                }

                if (ticket.Estado != "CERRADO")
                {
                    success = false;
                    message = "Solo se puede facturar un ticket cerrado.";
                    return;
                }

                bool yaFacturado = db.Factura.Any(x => x.IdTicket == entity.IdTicket);

                if (yaFacturado)
                {
                    success = false;
                    message = "Ese ticket ya fue facturado.";
                    return;
                }

                decimal subtotal = ticket.MontoCalculado;
                decimal impuesto = Math.Round(subtotal * 0.15m, 2);
                decimal total = subtotal + impuesto;

                entity.IdCliente = ticket.IdCliente;
                entity.FechaEmision = DateTime.Now;
                entity.Subtotal = subtotal;
                entity.Impuesto = impuesto;
                entity.Total = total;
                entity.Estado = "EMITIDA";

                if (string.IsNullOrWhiteSpace(entity.Serie))
                {
                    entity.Serie = "A";
                }

                if (string.IsNullOrWhiteSpace(entity.Folio))
                {
                    int siguiente = db.Factura.Count() + 1;
                    entity.Folio = "FAC-" + siguiente.ToString("00000");
                }

                db.Factura.Add(entity);
                db.SaveChanges();

                success = true;
                message = "Factura registrada correctamente.";
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
                eFactura entity = db.Factura
                    .Where(x => x.Id == id)
                    .FirstOrDefault();

                if (entity != null)
                {
                    db.Factura.Remove(entity);
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

        public List<eTicket> toListTicket()
        {
            return db.Ticket
                .Where(x => x.Estado == "CERRADO")
                .ToList();
        }
    }
}