using DAO;
using Entity.Estructura;
using Entity.General;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Factory
{
    public class fEspacio
    {
        DbContextG2 db { get; set; }

        public bool success { get; set; }
        public string message { get; set; }

        public fEspacio()
        {
            db = new DbContextG2();
            success = false;
            message = "";
        }

        public List<eEspacio> toList(string text)
        {
            if (text == null)
            {
                text = "";
            }

            List<eEspacio> list = db.Espacio
                .Include("Zona")
                .Include("TipoVehiculo")
                .Where(x => x.Codigo.Contains(text)
                         || (x.Zona != null && x.Zona.Nombre.Contains(text))
                         || (x.TipoVehiculo != null && x.TipoVehiculo.Nombre.Contains(text)))
                .ToList();

            return list;
        }

        public eEspacio getForId(int id)
        {
            eEspacio entity = db.Espacio
                .Include("Zona")
                .Include("TipoVehiculo")
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return entity;
        }

        public void save(eEspacio entity)
        {
            try
            {
                // 🔥 VALIDACIÓN: no repetir código en misma zona
                bool existe = db.Espacio.Any(x =>
                    x.Codigo == entity.Codigo &&
                    x.IdZona == entity.IdZona &&
                    x.Id != entity.Id);

                if (existe)
                {
                    success = false;
                    message = "Ya existe un espacio con ese código en la zona.";
                    return;
                }

                if (entity.Id == 0)
                {
                    entity.Disponible = true; // default
                    db.Espacio.Add(entity);
                }
                else
                {
                    eEspacio obj = db.Espacio
                        .Where(x => x.Id == entity.Id)
                        .FirstOrDefault();

                    if (obj != null)
                    {
                        obj.IdZona = entity.IdZona;
                        obj.IdTipoVehiculo = entity.IdTipoVehiculo;
                        obj.Codigo = entity.Codigo;
                        obj.Disponible = entity.Disponible;
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
                eEspacio entity = db.Espacio
                    .Where(x => x.Id == id)
                    .FirstOrDefault();

                if (entity != null)
                {
                    db.Espacio.Remove(entity);
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

        public List<eZona> toListZona()
        {
            return db.Zona.ToList();
        }

        public List<eTipoVehiculo> toListTipoVehiculo()
        {
            return db.TipoVehiculo.ToList();
        }
    }
}