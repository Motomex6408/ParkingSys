using DAO;
using Entity.Administrador;
using Entity.Estructura;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Factory
{
    public class fUsuario
    {
        DbContextG2 db { get; set; }

        public bool success { get; set; }
        public string message { get; set; }

        public fUsuario()
        {
            db = new DbContextG2();
            success = false;
            message = "";
        }

        public List<eUsuario> toList(string text)
        {
            if (text == null)
            {
                text = "";
            }

            List<eUsuario> list = db.Usuario
                .Include("Rol")
                .Include("Sucursal")
                .Where(x => x.Nombre.Contains(text)
                         || x.Apellido.Contains(text)
                         || x.Email.Contains(text)
                         || (x.Rol != null && x.Rol.Nombre.Contains(text))
                         || (x.Sucursal != null && x.Sucursal.Nombre.Contains(text)))
                .ToList();

            return list;
        }

        public eUsuario getForId(int id)
        {
            eUsuario entity = db.Usuario
                .Include("Rol")
                .Include("Sucursal")
                .Where(x => x.Id == id)
                .FirstOrDefault();

            return entity;
        }

        public void save(eUsuario entity)
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

                eRol rol = db.Rol
                    .Where(x => x.Id == entity.IdRol)
                    .FirstOrDefault();

                if (rol == null)
                {
                    success = false;
                    message = "El rol seleccionado no es válido.";
                    return;
                }

                bool existe = db.Usuario.Any(x =>
                    x.Email == entity.Email &&
                    x.Id != entity.Id);

                if (existe)
                {
                    success = false;
                    message = "Ya existe un usuario con ese correo.";
                    return;
                }

                if (entity.Id == 0)
                {
                    if (string.IsNullOrWhiteSpace(entity.Password))
                    {
                        success = false;
                        message = "La clave es obligatoria.";
                        return;
                    }

                    entity.Password = PasswordHash.HashPassword(entity.Password);
                    db.Usuario.Add(entity);
                }

                
                else
                {
                    eUsuario obj = db.Usuario
                        .Where(x => x.Id == entity.Id)
                        .FirstOrDefault();

                    if (obj != null)
                    {
                        obj.IdSucursal = entity.IdSucursal;
                        obj.IdRol = entity.IdRol;
                        obj.Nombre = entity.Nombre;
                        obj.Apellido = entity.Apellido;
                        obj.Email = entity.Email;
                        obj.Activo = entity.Activo;

                        
                        if (!string.IsNullOrWhiteSpace(entity.Password))
                        {
                            obj.Password = PasswordHash.HashPassword(entity.Password);
                        }
                    }

                    if (!string.IsNullOrEmpty(entity.Foto))
                    {
                        obj.Foto = entity.Foto;
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
                eUsuario entity = db.Usuario
                    .Where(x => x.Id == id)
                    .FirstOrDefault();

                if (entity != null)
                {
                    db.Usuario.Remove(entity);
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

        public List<eRol> toListRol()
        {
            List<eRol> list = db.Rol.ToList();
            return list;
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