namespace AeropuertoApp.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialScript : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Aerolinea",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Avion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        CapacidadDisponible = c.Int(nullable: false),
                        AerolineaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Aerolinea", t => t.AerolineaId)
                .Index(t => t.AerolineaId);
            
            CreateTable(
                "dbo.Vuelo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Numero = c.String(),
                        AvionId = c.Int(nullable: false),
                        FechaSalida = c.DateTime(nullable: false),
                        FechaLlegada = c.DateTime(nullable: false),
                        AeropuertoOrigenId = c.Int(nullable: false),
                        AeropuertoDestinoId = c.Int(nullable: false),
                        Costo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PorcExtraNormal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PorcExtraEjecutivo = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Aeropuerto", t => t.AeropuertoDestinoId)
                .ForeignKey("dbo.Aeropuerto", t => t.AeropuertoOrigenId)
                .ForeignKey("dbo.Avion", t => t.AvionId)
                .Index(t => t.AvionId)
                .Index(t => t.AeropuertoOrigenId)
                .Index(t => t.AeropuertoDestinoId);
            
            CreateTable(
                "dbo.Aeropuerto",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Ubicacion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DetalleOperacion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OperacionId = c.Int(nullable: false),
                        VueloId = c.Int(nullable: false),
                        Costo = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Operacion", t => t.OperacionId)
                .ForeignKey("dbo.Vuelo", t => t.VueloId)
                .Index(t => t.OperacionId)
                .Index(t => t.VueloId);
            
            CreateTable(
                "dbo.Operacion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClienteId = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        Costo = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Pagado = c.Boolean(nullable: false),
                        FormaPago = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cliente", t => t.ClienteId)
                .Index(t => t.ClienteId);
            
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        PrimerApellido = c.String(),
                        SegundoApellido = c.String(),
                        TipoCliente = c.Int(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pasajero",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Folio = c.String(),
                        NumeroAsiento = c.Int(nullable: false),
                        Nombre = c.String(),
                        PrimerApellido = c.String(),
                        SegundoApellido = c.String(),
                        VueloId = c.Int(nullable: false),
                        OperacionId = c.Int(nullable: false),
                        TipoPasajero = c.Int(nullable: false),
                        Disponible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Operacion", t => t.OperacionId)
                .ForeignKey("dbo.Vuelo", t => t.VueloId)
                .Index(t => t.VueloId)
                .Index(t => t.OperacionId);
            
            CreateTable(
                "dbo.ConfigGenVuelo",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Valor = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DetalleOperacion", "VueloId", "dbo.Vuelo");
            DropForeignKey("dbo.Pasajero", "VueloId", "dbo.Vuelo");
            DropForeignKey("dbo.Pasajero", "OperacionId", "dbo.Operacion");
            DropForeignKey("dbo.DetalleOperacion", "OperacionId", "dbo.Operacion");
            DropForeignKey("dbo.Operacion", "ClienteId", "dbo.Cliente");
            DropForeignKey("dbo.Cliente", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.Vuelo", "AvionId", "dbo.Avion");
            DropForeignKey("dbo.Vuelo", "AeropuertoOrigenId", "dbo.Aeropuerto");
            DropForeignKey("dbo.Vuelo", "AeropuertoDestinoId", "dbo.Aeropuerto");
            DropForeignKey("dbo.Avion", "AerolineaId", "dbo.Aerolinea");
            DropIndex("dbo.Pasajero", new[] { "OperacionId" });
            DropIndex("dbo.Pasajero", new[] { "VueloId" });
            DropIndex("dbo.Cliente", new[] { "UsuarioId" });
            DropIndex("dbo.Operacion", new[] { "ClienteId" });
            DropIndex("dbo.DetalleOperacion", new[] { "VueloId" });
            DropIndex("dbo.DetalleOperacion", new[] { "OperacionId" });
            DropIndex("dbo.Vuelo", new[] { "AeropuertoDestinoId" });
            DropIndex("dbo.Vuelo", new[] { "AeropuertoOrigenId" });
            DropIndex("dbo.Vuelo", new[] { "AvionId" });
            DropIndex("dbo.Avion", new[] { "AerolineaId" });
            DropTable("dbo.ConfigGenVuelo");
            DropTable("dbo.Pasajero");
            DropTable("dbo.Usuario");
            DropTable("dbo.Cliente");
            DropTable("dbo.Operacion");
            DropTable("dbo.DetalleOperacion");
            DropTable("dbo.Aeropuerto");
            DropTable("dbo.Vuelo");
            DropTable("dbo.Avion");
            DropTable("dbo.Aerolinea");
        }
    }
}
