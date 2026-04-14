using DAO;
using Entity.Administrador;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;

namespace Factory
{
    public class fAuditoria
    {
        DbContextG2 db { get; set; }

        public bool success { get; set; }
        public string message { get; set; }

        public fAuditoria()
        {
            db = new DbContextG2();
            success = false;
            message = "";
        }

        public void save(int idUsuario, string modulo, string accion, string descripcion)
        {
            try
            {
                db.Database.ExecuteSqlCommand(
                    "EXEC sp_Auditoria_Insertar @IdUsuario, @Modulo, @Accion, @Descripcion",
                    new SqlParameter("@IdUsuario", idUsuario),
                    new SqlParameter("@Modulo", modulo),
                    new SqlParameter("@Accion", accion),
                    new SqlParameter("@Descripcion", descripcion)
                );

                success = true;
                message = "Auditoría registrada correctamente.";
            }
            catch (System.Exception ex)
            {
                success = false;
                message = ex.Message;
            }
        }

        public List<eAuditoria> toList(string text)
        {
            if (text == null)
            {
                text = "";
            }

            List<eAuditoria> list = db.Auditoria
                .Include("Usuario")
                .Where(x => x.Modulo.Contains(text)
                         || x.Accion.Contains(text)
                         || x.Descripcion.Contains(text)
                         || (x.Usuario != null && x.Usuario.Nombre.Contains(text))
                         || (x.Usuario != null && x.Usuario.Apellido.Contains(text)))
                .OrderByDescending(x => x.Fecha)
                .ToList();

            return list;
        }

        public eAuditoria getForId(int id)
        {
            eAuditoria entity = db.Auditoria
                .Include("Usuario")
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return entity;
        }
    }
}