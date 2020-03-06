namespace AeropuertoApp.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AeropuertoApp.EntityFramework.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AeropuertoApp.EntityFramework.AppDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            var usuario1 = new Domain.Usuario()
            {
                Id = 1,
                Username = "jacosgalla",
                Password = "0000"
            };
            context.Usuario.Add(usuario1);
            var usuario2 = new Domain.Usuario()
            {
                Id = 2,
                Username = "anonimo1",
                Password = "0000"
            };
            context.Usuario.Add(usuario2);

            context.Cliente.Add(new Domain.Cliente()
            {
                Id = 1,
                Nombre = "Jose Alberto",
                PrimerApellido = "Cosgalla",
                SegundoApellido = "Escalante",
                TipoCliente = Domain.TipoCliente.Normal,
                Usuario = usuario1
            });

            context.Cliente.Add(new Domain.Cliente()
            {
                Id = 2,
                Nombre = "Anonimo",
                PrimerApellido = "Perez",
                SegundoApellido = "Perez",
                TipoCliente = Domain.TipoCliente.Premium,
                Usuario = usuario2
            });

            context.Aerolinea.Add(new Domain.Aerolinea()
            {
                Id = 1,
                Nombre = "Interjet"
            });

            context.Aeropuerto.Add(new Domain.Aeropuerto()
            {
                Id = 1,
                Nombre = "Aeropuerto de campeche",
                Ubicacion = "Campeche"
            });

            context.Aeropuerto.Add(new Domain.Aeropuerto()
            {
                Id = 2,
                Nombre = "Aeropuerto de ciudad de mexico",
                Ubicacion = "Ciudad de mexico"
            });

            context.Aeropuerto.Add(new Domain.Aeropuerto()
            {
                Id = 3,
                Nombre = "Aeropuerto de morelia",
                Ubicacion = "Morelia"
            });

            context.Aeropuerto.Add(new Domain.Aeropuerto()
            {
                Id = 4,
                Nombre = "Aeropuerto de veracruz",
                Ubicacion = "Veracruz"
            });

            context.Avion.Add(new Domain.Avion()
            {
                Id = 1,
                Nombre = "Avion 001",
                AerolineaId = 1,
                CapacidadDisponible = 100
            });

            context.Vuelo.Add(new Domain.Vuelo()
            {
                Id = 1,
                AvionId = 1,
                Numero = "01",
                FechaSalida = new DateTime(2020, 3, 5, 10, 20, 0),
                FechaLlegada = new DateTime(2020, 3, 5, 12, 40, 0),
                AeropuertoOrigenId = 1,
                AeropuertoDestinoId = 2,
                Costo = 5300,
                PorcExtraNormal = 35,
                PorcExtraEjecutivo = 45
            });

            context.Vuelo.Add(new Domain.Vuelo()
            {
                Id = 1,
                AvionId = 1,
                Numero = "02",
                FechaSalida = new DateTime(2020, 3, 7, 18, 15, 0),
                FechaLlegada = new DateTime(2020, 3, 7, 23, 45, 0),
                AeropuertoOrigenId = 1,
                AeropuertoDestinoId = 2,
                Costo = 5300,
                PorcExtraNormal = 35,
                PorcExtraEjecutivo = 45
            });
        }
    }
}
