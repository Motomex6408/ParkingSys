using DAO;
using Entity.Estructura;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Factory
{
    public class fZona
    {
        DbContextG2 db { get; set; }

        public bool success { get; set; }
        public string message { get; set; }

        public fZona()
        {
            db = new DbContextG2();
            success = false;
            message = "";
        }

        public List<eZona> toList(string text)
        {
            if (text == null)
            {
                text = "";
            }

            List<eZona> list = db.Zona
                .Include("Sucursal")
                .Where(x => x.Nombre.Contains(text)
                         || (x.Descripcion != null && x.Descripcion.Contains(text))
                         || (x.Sucursal != null && x.Sucursal.Nombre.Contains(text)))
                .ToList();

            return list;
        }

        public eZona getForId(int id)
        {
            eZona entity = db.Zona
                .Include("Sucursal")
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return entity;
        }

        public void save(eZona entity)
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

                if (entity.Id == 0)
                {
                    db.Zona.Add(entity);
                }
                else
                {
                    eZona obj = db.Zona
                        .Where(x => x.Id == entity.Id)
                        .FirstOrDefault();

                    if (obj != null)
                    {
                        obj.IdSucursal = entity.IdSucursal;
                        obj.Nombre = entity.Nombre;
                        obj.Descripcion = entity.Descripcion;
                        obj.Nivel = entity.Nivel;
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
                eZona entity = db.Zona
                    .Where(x => x.Id == id)
                    .FirstOrDefault();

                if (entity != null)
                {
                    db.Zona.Remove(entity);
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
    }
}