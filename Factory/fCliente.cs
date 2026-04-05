using DAO;
using Entity.General;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Factory
{
    public class fCliente
    {
        DbContextG2 db { get; set; }

        public bool success { get; set; }
        public string message { get; set; }

        public fCliente()
        {
            db = new DbContextG2();
            success = false;
            message = "";
        }

        public List<eCliente> toList(string text)
        {
            if (text == null)
            {
                text = "";
            }

            List<eCliente> list = db.Cliente
                .Where(x => x.Nombre.Contains(text)
                         || x.Apellido.Contains(text)
                         || x.Email.Contains(text)
                         || x.Telefono.Contains(text)
                         || x.RTN.Contains(text)
                         || x.Tipo.Contains(text))
                .ToList();

            return list;
        }

        public eCliente getForId(int id)
        {
            eCliente entity = db.Cliente
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return entity;
        }

        public void save(eCliente entity)
        {
            try
            {
                if (entity.Id == 0)
                {
                    db.Cliente.Add(entity);
                }
                else
                {
                    eCliente obj = db.Cliente
                        .Where(x => x.Id == entity.Id)
                        .FirstOrDefault();

                    if (obj != null)
                    {
                        obj.Nombre = entity.Nombre;
                        obj.Apellido = entity.Apellido;
                        obj.Telefono = entity.Telefono;
                        obj.Email = entity.Email;
                        obj.RTN = entity.RTN;
                        obj.Tipo = entity.Tipo;
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
                eCliente entity = db.Cliente
                    .Where(x => x.Id == id)
                    .FirstOrDefault();

                if (entity != null)
                {
                    db.Cliente.Remove(entity);
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