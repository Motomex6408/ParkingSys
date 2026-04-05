using Entity.Administrador;
using Entity.Estructura;
using Entity.Facturacion;
using Entity.General;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.SqlClient;

namespace DAO
{
    public class DbContextG2 : DbContext
    {
        public static SqlConnection conn = new SqlConnection(@"Data Source=ANDERSON\SQLSERVER; uid=sa; pwd=saa; database=ParkingG2;");

        public DbContextG2() : base(conn.ConnectionString)
        {
        }

        public DbSet<eEmpresa> Empresa { get; set; }
        public DbSet<eSucursal> Sucursal { get; set; }
        public DbSet<eZona> Zona { get; set; }
        public DbSet<eEspacio> Espacio { get; set; }

        public DbSet<eRol> Rol { get; set; }
        public DbSet<eUsuario> Usuario { get; set; }
        public DbSet<eTarifa> Tarifa { get; set; }

        public DbSet<eCliente> Cliente { get; set; }
        public DbSet<eTipoVehiculo> TipoVehiculo { get; set; }
        public DbSet<eVehiculo> Vehiculo { get; set; }
        public DbSet<eTicket> Ticket { get; set; }
        public DbSet<eKardex> Kardex { get; set; }

        public DbSet<eMetodoPago> MetodoPago { get; set; }
        public DbSet<eFactura> Factura { get; set; }
        public DbSet<eDetalleFactura> DetalleFactura { get; set; }
        public DbSet<ePago> Pago { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}