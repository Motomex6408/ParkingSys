using DAO;
using System.Data.Entity;
using Entity.General;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Factory
{
    public class fVehiculo
    {
        DbContextG2 db { get; set; }

        public bool success { get; set; }
        public string message { get; set; }

        public fVehiculo()
        {
            db = new DbContextG2();
            success = false;
            message = "";
        }

        public List<eVehiculo> toList(string text)
        {
            if (text == null)
            {
                text = "";
            }

            List<eVehiculo> list = db.Vehiculo
                .Include("TipoVehiculo")
                .Where(x => x.Placa.Contains(text)
                         || (x.Marca != null && x.Marca.Contains(text))
                         || (x.Modelo != null && x.Modelo.Contains(text))
                         || (x.Color != null && x.Color.Contains(text))
                         || (x.TipoVehiculo != null && x.TipoVehiculo.Nombre.Contains(text)))
                .ToList();

            return list;
        }

        public eVehiculo getForId(int id)
        {
            eVehiculo entity = db.Vehiculo
                .Include("TipoVehiculo")
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return entity;
        }

        public void save(eVehiculo entity)
        {
            try
            {
                if (entity.Id == 0)
                {
                    db.Vehiculo.Add(entity);
                }
                else
                {
                    eVehiculo obj = db.Vehiculo
                        .Where(x => x.Id == entity.Id)
                        .FirstOrDefault();

                    if (obj != null)
                    {
                        obj.Placa = entity.Placa;
                        obj.IdTipoVehiculo = entity.IdTipoVehiculo;
                        obj.Marca = entity.Marca;
                        obj.Modelo = entity.Modelo;
                        obj.Color = entity.Color;
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
                eVehiculo entity = db.Vehiculo
                    .Where(x => x.Id == id)
                    .FirstOrDefault();

                if (entity != null)
                {
                    db.Vehiculo.Remove(entity);
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

        public List<eTipoVehiculo> toListTipoVehiculo()
        {
            List<eTipoVehiculo> list = db.TipoVehiculo.ToList();
            return list;
        }
    }
}