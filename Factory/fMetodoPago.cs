using DAO;
using Entity.Facturacion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Factory
{
    public class fMetodoPago
    {
        DbContextG2 db { get; set; }

        public bool success { get; set; }
        public string message { get; set; }

        public fMetodoPago()
        {
            db = new DbContextG2();
            success = false;
            message = "";
        }

        public List<eMetodoPago> toList(string text)
        {
            if (text == null)
            {
                text = "";
            }

            List<eMetodoPago> list = db.MetodoPago
                .Where(x => x.Nombre.Contains(text)
                         || x.Descripcion.Contains(text))
                .ToList();

            return list;
        }

        public eMetodoPago getForId(int id)
        {
            eMetodoPago entity = db.MetodoPago
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return entity;
        }

        public void save(eMetodoPago entity)
        {
            try
            {
                if (entity.Id == 0)
                {
                    db.MetodoPago.Add(entity);
                }
                else
                {
                    eMetodoPago obj = db.MetodoPago
                        .Where(x => x.Id == entity.Id)
                        .FirstOrDefault();

                    if (obj != null)
                    {
                        obj.Nombre = entity.Nombre;
                        obj.Descripcion = entity.Descripcion;
                        obj.Activo = entity.Activo;
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
                eMetodoPago entity = db.MetodoPago
                    .Where(x => x.Id == id)
                    .FirstOrDefault();

                if (entity != null)
                {
                    db.MetodoPago.Remove(entity);
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