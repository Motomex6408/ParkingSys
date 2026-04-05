using System;
using System.Collections.Generic;
using System.Linq;
using DAO;
using Entity.General;
using System.Text;
using System.Threading.Tasks;

namespace Factory
{
    public class fTipoVehiculo
    {
        DbContextG2 db { get; set; }

        public bool success { get; set; }
        public string message { get; set; }

        public fTipoVehiculo()
        {
            db = new DbContextG2();
            success = false;
            message = "";
        }

        public List<eTipoVehiculo> toList(string text)
        {
            List<eTipoVehiculo> list = db.TipoVehiculo
                .Where(x => x.Nombre.Contains(text))
                .ToList();

            return list;
        }

        public eTipoVehiculo getForId(int id)
        {
            eTipoVehiculo entity = db.TipoVehiculo
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return entity;
        }

        public void save(eTipoVehiculo entity)
        {
            try
            {
                if (entity.Id == 0)
                {
                    db.TipoVehiculo.Add(entity);
                }
                else
                {
                    eTipoVehiculo obj = db.TipoVehiculo
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
                eTipoVehiculo entity = db.TipoVehiculo
                    .Where(x => x.Id == id)
                    .FirstOrDefault();

                if (entity != null)
                {
                    db.TipoVehiculo.Remove(entity);
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