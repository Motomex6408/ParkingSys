namespace DAO.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Primera : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 60),
                        Apellido = c.String(nullable: false, maxLength: 60),
                        Telefono = c.String(maxLength: 30),
                        Email = c.String(maxLength: 100),
                        RFC = c.String(maxLength: 30),
                        Tipo = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DetalleFactura",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdFactura = c.Int(nullable: false),
                        Descripcion = c.String(nullable: false, maxLength: 150),
                        Cantidad = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrecioUnitario = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Factura", t => t.IdFactura)
                .Index(t => t.IdFactura);
            
            CreateTable(
                "dbo.Factura",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdTicket = c.Int(nullable: false),
                        IdCliente = c.Int(nullable: false),
                        Folio = c.String(nullable: false, maxLength: 30),
                        Serie = c.String(maxLength: 20),
                        FechaEmision = c.DateTime(nullable: false),
                        Subtotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Impuesto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Estado = c.String(maxLength: 30),
                        UUIDFiscal = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cliente", t => t.IdCliente)
                .ForeignKey("dbo.Ticket", t => t.IdTicket)
                .Index(t => t.IdTicket)
                .Index(t => t.IdCliente);
            
            CreateTable(
                "dbo.Ticket",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdEspacio = c.Int(nullable: false),
                        IdVehiculo = c.Int(nullable: false),
                        IdCliente = c.Int(nullable: false),
                        IdTarifa = c.Int(nullable: false),
                        IdUsuario = c.Int(nullable: false),
                        Entrada = c.DateTime(nullable: false),
                        Salida = c.DateTime(),
                        MinutosTotales = c.Int(nullable: false),
                        MontoCalculado = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Estado = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cliente", t => t.IdCliente)
                .ForeignKey("dbo.Espacio", t => t.IdEspacio)
                .ForeignKey("dbo.Tarifa", t => t.IdTarifa)
                .ForeignKey("dbo.Usuario", t => t.IdUsuario)
                .ForeignKey("dbo.Vehiculo", t => t.IdVehiculo)
                .Index(t => t.IdEspacio)
                .Index(t => t.IdVehiculo)
                .Index(t => t.IdCliente)
                .Index(t => t.IdTarifa)
                .Index(t => t.IdUsuario);
            
            CreateTable(
                "dbo.Espacio",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdZona = c.Int(nullable: false),
                        Codigo = c.String(nullable: false, maxLength: 20),
                        IdTipoVehiculo = c.Int(nullable: false),
                        Disponible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TipoVehiculo", t => t.IdTipoVehiculo)
                .ForeignKey("dbo.Zona", t => t.IdZona)
                .Index(t => t.IdZona)
                .Index(t => t.IdTipoVehiculo);
            
            CreateTable(
                "dbo.TipoVehiculo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 60),
                        Descripcion = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Zona",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdSucursal = c.Int(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 60),
                        Descripcion = c.String(maxLength: 150),
                        Nivel = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sucursal", t => t.IdSucursal)
                .Index(t => t.IdSucursal);
            
            CreateTable(
                "dbo.Sucursal",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdEmpresa = c.Int(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        Direccion = c.String(nullable: false, maxLength: 150),
                        Telefono = c.String(maxLength: 30),
                        Email = c.String(maxLength: 100),
                        CapacidadTotal = c.Int(nullable: false),
                        Activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Empresa", t => t.IdEmpresa)
                .Index(t => t.IdEmpresa);
            
            CreateTable(
                "dbo.Empresa",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 100),
                        RFC = c.String(nullable: false, maxLength: 30),
                        DireccionFiscal = c.String(nullable: false, maxLength: 150),
                        Telefono = c.String(maxLength: 30),
                        Email = c.String(maxLength: 100),
                        Activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tarifa",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdSucursal = c.Int(nullable: false),
                        IdTipoVehiculo = c.Int(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 60),
                        PrecioHora = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrecioDia = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PrecioMes = c.Decimal(nullable: false, precision: 18, scale: 2),
                        VigenciaInicio = c.DateTime(nullable: false),
                        VigenciaFin = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sucursal", t => t.IdSucursal)
                .ForeignKey("dbo.TipoVehiculo", t => t.IdTipoVehiculo)
                .Index(t => t.IdSucursal)
                .Index(t => t.IdTipoVehiculo);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdSucursal = c.Int(nullable: false),
                        IdRol = c.Int(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 60),
                        Apellido = c.String(nullable: false, maxLength: 60),
                        Email = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 100),
                        Activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rol", t => t.IdRol)
                .ForeignKey("dbo.Sucursal", t => t.IdSucursal)
                .Index(t => t.IdSucursal)
                .Index(t => t.IdRol);
            
            CreateTable(
                "dbo.Rol",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 60),
                        Descripcion = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Vehiculo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Placa = c.String(nullable: false, maxLength: 20),
                        IdTipoVehiculo = c.Int(nullable: false),
                        Marca = c.String(maxLength: 60),
                        Modelo = c.String(maxLength: 60),
                        Color = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TipoVehiculo", t => t.IdTipoVehiculo)
                .Index(t => t.IdTipoVehiculo);
            
            CreateTable(
                "dbo.Kardex",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdTicket = c.Int(nullable: false),
                        TipoMovimiento = c.String(nullable: false, maxLength: 50),
                        Fecha = c.DateTime(nullable: false),
                        IdEspacioAnterior = c.Int(),
                        IdEspacioNuevo = c.Int(),
                        Observacion = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Espacio", t => t.IdEspacioAnterior)
                .ForeignKey("dbo.Espacio", t => t.IdEspacioNuevo)
                .ForeignKey("dbo.Ticket", t => t.IdTicket)
                .Index(t => t.IdTicket)
                .Index(t => t.IdEspacioAnterior)
                .Index(t => t.IdEspacioNuevo);
            
            CreateTable(
                "dbo.MetodoPago",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 60),
                        Descripcion = c.String(maxLength: 150),
                        Activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pago",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdFactura = c.Int(nullable: false),
                        IdMetodoPago = c.Int(nullable: false),
                        Monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FechaPago = c.DateTime(nullable: false),
                        Referencia = c.String(maxLength: 60),
                        Estado = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Factura", t => t.IdFactura)
                .ForeignKey("dbo.MetodoPago", t => t.IdMetodoPago)
                .Index(t => t.IdFactura)
                .Index(t => t.IdMetodoPago);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pago", "IdMetodoPago", "dbo.MetodoPago");
            DropForeignKey("dbo.Pago", "IdFactura", "dbo.Factura");
            DropForeignKey("dbo.Kardex", "IdTicket", "dbo.Ticket");
            DropForeignKey("dbo.Kardex", "IdEspacioNuevo", "dbo.Espacio");
            DropForeignKey("dbo.Kardex", "IdEspacioAnterior", "dbo.Espacio");
            DropForeignKey("dbo.DetalleFactura", "IdFactura", "dbo.Factura");
            DropForeignKey("dbo.Factura", "IdTicket", "dbo.Ticket");
            DropForeignKey("dbo.Ticket", "IdVehiculo", "dbo.Vehiculo");
            DropForeignKey("dbo.Vehiculo", "IdTipoVehiculo", "dbo.TipoVehiculo");
            DropForeignKey("dbo.Ticket", "IdUsuario", "dbo.Usuario");
            DropForeignKey("dbo.Usuario", "IdSucursal", "dbo.Sucursal");
            DropForeignKey("dbo.Usuario", "IdRol", "dbo.Rol");
            DropForeignKey("dbo.Ticket", "IdTarifa", "dbo.Tarifa");
            DropForeignKey("dbo.Tarifa", "IdTipoVehiculo", "dbo.TipoVehiculo");
            DropForeignKey("dbo.Tarifa", "IdSucursal", "dbo.Sucursal");
            DropForeignKey("dbo.Ticket", "IdEspacio", "dbo.Espacio");
            DropForeignKey("dbo.Espacio", "IdZona", "dbo.Zona");
            DropForeignKey("dbo.Zona", "IdSucursal", "dbo.Sucursal");
            DropForeignKey("dbo.Sucursal", "IdEmpresa", "dbo.Empresa");
            DropForeignKey("dbo.Espacio", "IdTipoVehiculo", "dbo.TipoVehiculo");
            DropForeignKey("dbo.Ticket", "IdCliente", "dbo.Cliente");
            DropForeignKey("dbo.Factura", "IdCliente", "dbo.Cliente");
            DropIndex("dbo.Pago", new[] { "IdMetodoPago" });
            DropIndex("dbo.Pago", new[] { "IdFactura" });
            DropIndex("dbo.Kardex", new[] { "IdEspacioNuevo" });
            DropIndex("dbo.Kardex", new[] { "IdEspacioAnterior" });
            DropIndex("dbo.Kardex", new[] { "IdTicket" });
            DropIndex("dbo.Vehiculo", new[] { "IdTipoVehiculo" });
            DropIndex("dbo.Usuario", new[] { "IdRol" });
            DropIndex("dbo.Usuario", new[] { "IdSucursal" });
            DropIndex("dbo.Tarifa", new[] { "IdTipoVehiculo" });
            DropIndex("dbo.Tarifa", new[] { "IdSucursal" });
            DropIndex("dbo.Sucursal", new[] { "IdEmpresa" });
            DropIndex("dbo.Zona", new[] { "IdSucursal" });
            DropIndex("dbo.Espacio", new[] { "IdTipoVehiculo" });
            DropIndex("dbo.Espacio", new[] { "IdZona" });
            DropIndex("dbo.Ticket", new[] { "IdUsuario" });
            DropIndex("dbo.Ticket", new[] { "IdTarifa" });
            DropIndex("dbo.Ticket", new[] { "IdCliente" });
            DropIndex("dbo.Ticket", new[] { "IdVehiculo" });
            DropIndex("dbo.Ticket", new[] { "IdEspacio" });
            DropIndex("dbo.Factura", new[] { "IdCliente" });
            DropIndex("dbo.Factura", new[] { "IdTicket" });
            DropIndex("dbo.DetalleFactura", new[] { "IdFactura" });
            DropTable("dbo.Pago");
            DropTable("dbo.MetodoPago");
            DropTable("dbo.Kardex");
            DropTable("dbo.Vehiculo");
            DropTable("dbo.Rol");
            DropTable("dbo.Usuario");
            DropTable("dbo.Tarifa");
            DropTable("dbo.Empresa");
            DropTable("dbo.Sucursal");
            DropTable("dbo.Zona");
            DropTable("dbo.TipoVehiculo");
            DropTable("dbo.Espacio");
            DropTable("dbo.Ticket");
            DropTable("dbo.Factura");
            DropTable("dbo.DetalleFactura");
            DropTable("dbo.Cliente");
        }
    }
}
