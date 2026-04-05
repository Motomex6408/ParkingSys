using DAO;
using Entity.Administrador;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Factory
{
    public class fRol
    {
        DbContextG2 db { get; set; }

        public bool success { get; set; }
        public string message { get; set; }

        public fRol()
        {
            db = new DbContextG2();
            success = false;
            message = "";
        }

        public List<eRol> toList(string text)
        {
            if (text == null)
            {
                text = "";
            }

            List<eRol> list = db.Rol
                .Where(x => x.Nombre.Contains(text)
                         || x.Descripcion.Contains(text))
                .ToList();

            return list;
        }

        public eRol getForId(int id)
        {
            eRol entity = db.Rol
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return entity;
        }

        public void save(eRol entity)
        {
            try
            {
                if (entity.Id == 0)
                {
                    db.Rol.Add(entity);
                }
                else
                {
                    eRol obj = db.Rol
                        .Where(x => x.Id == entity.Id)
                        .FirstOrDefault();

                    if (obj != null)
                    {
                        obj.Nombre = entity.Nombre;
                        obj.Descripcion = entity.Descripcion;
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
                eRol entity = db.Rol
                    .Where(x => x.Id == id)
                    .FirstOrDefault();

                if (entity != null)
                {
                    db.Rol.Remove(entity);
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
    }
}