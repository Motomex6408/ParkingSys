using DAO;
using Entity.General;
using Entity.Estructura;
using Entity.Administrador;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Factory
{
    public class fTicket
    {
        DbContextG2 db { get; set; }

        public bool success { get; set; }
        public string message { get; set; }

        public fTicket()
        {
            db = new DbContextG2();
            success = false;
            message = "";
        }

        public List<eTicket> toList(string text)
        {
            if (text == null)
            {
                text = "";
            }

            List<eTicket> list = db.Ticket
                .Include("Vehiculo")
                .Include("Cliente")
                .Include("Espacio")
                .Include("Usuario")
                .Include("Tarifa")
                .Where(x => x.Estado.Contains(text)
                         || (x.Vehiculo != null && x.Vehiculo.Placa.Contains(text))
                         || (x.Cliente != null && x.Cliente.Nombre.Contains(text))
                         || (x.Espacio != null && x.Espacio.Codigo.Contains(text)))
                .ToList();

            return list;
        }

        public eTicket getForId(int id)
        {
            eTicket entity = db.Ticket
                .Include("Vehiculo")
                .Include("Cliente")
                .Include("Espacio")
                .Include("Usuario")
                .Include("Tarifa")
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return entity;
        }

        public void save(eTicket entity)
        {
           
            try
            {
                eEspacio espacio = db.Espacio
                    .Where(x => x.Id == entity.IdEspacio)
                    .FirstOrDefault();

                if (espacio == null)
                {
                    success = false;
                    message = "El espacio seleccionado no existe.";
                    return;
                }

                if (!espacio.Disponible)
                {
                    success = false;
                    message = "El espacio seleccionado ya está ocupado.";
                    return;
                }

                eVehiculo vehiculo = db.Vehiculo
                    .Where(x => x.Id == entity.IdVehiculo)
                    .FirstOrDefault();

                if (vehiculo == null)
                {
                    success = false;
                    message = "El vehículo seleccionado no existe.";
                    return;
                }

                bool ticketActivo = db.Ticket.Any(x => x.IdVehiculo == entity.IdVehiculo && x.Estado == "ACTIVO");

                if (ticketActivo)
                {
                    success = false;
                    message = "El vehículo ya tiene un ticket activo.";
                    return;
                }

                entity.Entrada = DateTime.Now;
                entity.Salida = null;
                entity.MinutosTotales = 0;
                entity.MontoCalculado = 0;
                entity.Estado = "ACTIVO";



                db.Ticket.Add(entity);
                espacio.Disponible = false;

                db.SaveChanges();

                eKardex k = new eKardex();
                k.IdTicket = entity.Id;
                k.TipoMovimiento = "ENTRADA";
                k.Fecha = DateTime.Now;
                k.Monto = 0;
                k.Descripcion = "Ingreso de vehículo";

                db.Kardex.Add(k);
                db.SaveChanges();
                success = true;
                message = "Ticket registrado correctamente.";
            }
            catch (Exception ex)
            {
                success = false;
                message = ex.Message;
            }
        }

        public List<eVehiculo> toListVehiculo()
        {
            return db.Vehiculo.Include("TipoVehiculo").ToList();
        }

        public List<eCliente> toListCliente()
        {
            return db.Cliente.ToList();
        }

        public List<eEspacio> toListEspacio()
        {
            return db.Espacio
                .Where(x => x.Disponible == true)
                .Include("Zona")
                .ToList();
        }

        public List<eUsuario> toListUsuario()
        {
            return db.Usuario
                .Where(x => x.Activo == true)
                .ToList();
        }

        public List<eTarifa> toListTarifa()
        {
            return db.Tarifa
                .Include("TipoVehiculo")
                .Include("Sucursal")
                .ToList();
        }

        public void closeTicket(int id)
        {
            try
            {
                eTicket entity = db.Ticket
                    .Include("Espacio")
                    .Include("Tarifa")
                    .Where(x => x.Id == id)
                    .FirstOrDefault();

                if (entity == null)
                {
                    success = false;
                    message = "El ticket no existe.";
                    return;
                }

                if (entity.Estado == "CERRADO")
                {
                    success = false;
                    message = "El ticket ya fue cerrado.";
                    return;
                }

                entity.Salida = DateTime.Now;

                TimeSpan tiempo = entity.Salida.Value - entity.Entrada;
                int minutos = Convert.ToInt32(tiempo.TotalMinutes);

                if (minutos < 1)
                {
                    minutos = 1;
                }

                entity.MinutosTotales = minutos;

                decimal monto = 0;

                if (entity.Tarifa != null)
                {
                    decimal precioHora = entity.Tarifa.PrecioHora;
                    decimal horas = (decimal)minutos / 60m;
                    monto = Math.Ceiling(horas) * precioHora;
                }

                entity.MontoCalculado = monto;
                entity.Estado = "CERRADO";

                if (entity.Espacio != null)
                {
                    entity.Espacio.Disponible = true;
                }

                eKardex k = new eKardex();
                k.IdTicket = entity.Id;
                k.TipoMovimiento = "SALIDA";
                k.Fecha = DateTime.Now;
                k.Monto = entity.MontoCalculado;
                k.Descripcion = "Salida de vehículo";

                db.Kardex.Add(k);

                db.SaveChanges();

                success = true;
                message = "Ticket cerrado correctamente.";
            }
            catch (Exception ex)
            {
                success = false;
                message = ex.Message;
            }
        }
    }
}