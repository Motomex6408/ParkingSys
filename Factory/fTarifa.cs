using DAO;
using Entity.Administrador;
using Entity.Estructura;
using Entity.General;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Factory
{
    public class fTarifa
    {
        DbContextG2 db { get; set; }

        public bool success { get; set; }
        public string message { get; set; }

        public fTarifa()
        {
            db = new DbContextG2();
            success = false;
            message = "";
        }

        public List<eTarifa> toList(string text)
        {
            if (text == null)
            {
                text = "";
            }

            List<eTarifa> list = db.Tarifa
                .Include("Sucursal")
                .Include("TipoVehiculo")
                .Where(x => x.Nombre.Contains(text)
                         || (x.Sucursal != null && x.Sucursal.Nombre.Contains(text))
                         || (x.TipoVehiculo != null && x.TipoVehiculo.Nombre.Contains(text)))
                .ToList();

            return list;
        }

        public eTarifa getForId(int id)
        {
            eTarifa entity = db.Tarifa
                .Include("Sucursal")
                .Include("TipoVehiculo")
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return entity;
        }

        public void save(eTarifa entity)
        {
            try
            {
                eSucursal sucursal = db.Sucursal
                    .Where(x => x.Id == entity.IdSucursal && x.Activo == true)
                    .FirstOrDefault();

                if (sucursal == null)
                {
                    success = false;
                    message = "La sucursal seleccionada no está activa.";
                    return;
                }

                eTipoVehiculo tipoVehiculo = db.TipoVehiculo
                    .Where(x => x.Id == entity.IdTipoVehiculo)
                    .FirstOrDefault();

                if (tipoVehiculo == null)
                {
                    success = false;
                    message = "El tipo de vehículo no es válido.";
                    return;
                }

                if (entity.VigenciaFin < entity.VigenciaInicio)
                {
                    success = false;
                    message = "La vigencia final no puede ser menor que la vigencia inicial.";
                    return;
                }

                if (entity.Id == 0)
                {
                    db.Tarifa.Add(entity);
                }
                else
                {
                    eTarifa obj = db.Tarifa
                        .Where(x => x.Id == entity.Id)
                        .FirstOrDefault();

                    if (obj != null)
                    {
                        obj.IdSucursal = entity.IdSucursal;
                        obj.IdTipoVehiculo = entity.IdTipoVehiculo;
                        obj.Nombre = entity.Nombre;
                        obj.PrecioHora = entity.PrecioHora;
                        obj.PrecioDia = entity.PrecioDia;
                        obj.PrecioMes = entity.PrecioMes;
                        obj.VigenciaInicio = entity.VigenciaInicio;
                        obj.VigenciaFin = entity.VigenciaFin;
                    }
                }

                db.SaveChanges();
                success = true;
                message = "Guardado correctamente";
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
                eTarifa entity = db.Tarifa
                    .Where(x => x.Id == id)
                    .FirstOrDefault();

                if (entity != null)
                {
                    db.Tarifa.Remove(entity);
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

        public List<eSucursal> toListSucursal()
        {
            List<eSucursal> list = db.Sucursal
                .Where(x => x.Activo == true)
                .ToList();

            return list;
        }

        public List<eTipoVehiculo> toListTipoVehiculo()
        {
            List<eTipoVehiculo> list = db.TipoVehiculo.ToList();
            return list;
        }
    }
}