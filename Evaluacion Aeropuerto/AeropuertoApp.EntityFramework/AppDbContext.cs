using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using AeropuertoApp.Domain;
using AeropuertoApp.EntityFramework.Mappings;

namespace AeropuertoApp.EntityFramework
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=AppDbContext")
        {
            var type = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
            if (type == null)
                throw new Exception("Do not remove, ensures static reference to System.Data.Entity.SqlServer");

            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Aerolinea> Aerolinea { get; set; }
        public DbSet<Aeropuerto> Aeropuerto { get; set; }
        public DbSet<Avion> Avion { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<ConfiguracionGeneralVuelo> ConfiguracionGeneralVuelo { get; set; }
        public DbSet<Operacion> Operacion { get; set; }
        public DbSet<Pasajero> Pasajero { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Vuelo> Vuelo { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new AerolineaMapping());
            modelBuilder.Configurations.Add(new AeropuertoMapping());
            modelBuilder.Configurations.Add(new AvionMapping());
            modelBuilder.Configurations.Add(new ClienteMapping());
            modelBuilder.Configurations.Add(new ConfiguracionGeneralVueloMapping());
            modelBuilder.Configurations.Add(new OperacionMapping());
            modelBuilder.Configurations.Add(new PasajeroMapping());
            modelBuilder.Configurations.Add(new UsuarioMapping());
            modelBuilder.Configurations.Add(new VueloMapping());
        }
    }
}
