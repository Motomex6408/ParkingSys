using DAO;
using Entity.Estructura;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Factory
{
    public class fSucursal
    {
        DbContextG2 db { get; set; }

        public bool success { get; set; }
        public string message { get; set; }

        public fSucursal()
        {
            db = new DbContextG2();
            success = false;
            message = "";
        }

        public List<eSucursal> toList(string text)
        {
            if (text == null)
            {
                text = "";
            }

            List<eSucursal> list = db.Sucursal
                .Where(x => x.Nombre.Contains(text)
                         || x.Direccion.Contains(text))
                .ToList();

            return list;
        }

        public eSucursal getForId(int id)
        {
            eSucursal entity = db.Sucursal
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return entity;
        }

        public void save(eSucursal entity)
        {
            try
            {
               
                entity.IdEmpresa = 1;

                if (entity.Id == 0)
                {
                    db.Sucursal.Add(entity);
                }
                else
                {
                    eSucursal obj = db.Sucursal
                        .Where(x => x.Id == entity.Id)
                        .FirstOrDefault();

                    if (obj != null)
                    {
                        obj.Nombre = entity.Nombre;
                        obj.Direccion = entity.Direccion;
                        obj.Telefono = entity.Telefono;
                        obj.Email = entity.Email;
                        obj.CapacidadTotal = entity.CapacidadTotal;
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
                eSucursal entity = db.Sucursal
                    .Where(x => x.Id == id)
                    .FirstOrDefault();

                if (entity != null)
                {
                    db.Sucursal.Remove(entity);
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