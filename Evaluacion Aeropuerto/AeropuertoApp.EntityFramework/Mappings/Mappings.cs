using System.Data.Entity.ModelConfiguration;

namespace AeropuertoApp.EntityFramework.Mappings
{
    public class AerolineaMapping : EntityTypeConfiguration<Domain.Aerolinea>
    {
        public AerolineaMapping()
        {
            ToTable("Aerolinea").HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
        }
    }

    public class AeropuertoMapping : EntityTypeConfiguration<Domain.Aeropuerto>
    {
        public AeropuertoMapping()
        {
            ToTable("Aeropuerto").HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
        }
    }

    public class AvionMapping : EntityTypeConfiguration<Domain.Avion>
    {
        public AvionMapping()
        {
            ToTable("Avion").HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            HasRequired(avion => avion.Aerolinea).WithMany(aerolinea => aerolinea.Aviones).HasForeignKey(avion => avion.AerolineaId);
        }
    }

    public class ClienteMapping : EntityTypeConfiguration<Domain.Cliente>
    {
        public ClienteMapping()
        {
            ToTable("Cliente").HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
        }
    }

    public class ConfiguracionGeneralVueloMapping : EntityTypeConfiguration<Domain.ConfiguracionGeneralVuelo>
    {
        public ConfiguracionGeneralVueloMapping()
        {
            ToTable("ConfigGenVuelo").HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
        }
    }

    public class OperacionMapping : EntityTypeConfiguration<Domain.Operacion>
    {
        public OperacionMapping()
        {
            ToTable("Operacion").HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            HasRequired(operacion => operacion.Cliente).WithMany(cliente => cliente.Operaciones).HasForeignKey(operacion => operacion.ClienteId);
        }
    }

    public class PasajeroMapping : EntityTypeConfiguration<Domain.Pasajero>
    {
        public PasajeroMapping()
        {
            ToTable("Pasajero").HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            HasRequired(pasajero => pasajero.Operacion).WithMany(operacion => operacion.Pasajeros).HasForeignKey(pasajero => pasajero.OperacionId);
            HasRequired(pasajero => pasajero.Vuelo).WithMany(pasajero => pasajero.Pasajeros).HasForeignKey(pasajero => pasajero.VueloId);
        }
    }

    public class UsuarioMapping : EntityTypeConfiguration<Domain.Usuario>
    {
        public UsuarioMapping()
        {
            ToTable("Usuario").HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            HasOptional(usuario => usuario.Cliente).WithRequired(cliente => cliente.Usuario).Map(m => m.MapKey("UsuarioId"));
        }
    }

    public class VueloMapping : EntityTypeConfiguration<Domain.Vuelo>
    {
        public VueloMapping()
        {
            ToTable("Vuelo").HasKey(m => m.Id);
            Property(m => m.Id).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            HasRequired(vuelo => vuelo.AeropuertoOrigen).WithMany(aeropuerto => aeropuerto.VuelosOrigen).HasForeignKey(vuelo => vuelo.AeropuertoOrigenId);
            HasRequired(vuelo => vuelo.AeropuertoDestino).WithMany(aeropuerto => aeropuerto.VuelosDestino).HasForeignKey(vuelo => vuelo.AeropuertoDestinoId);
            HasRequired(vuelo => vuelo.Avion).WithMany(avion => avion.Vuelos).HasForeignKey(vuelo => vuelo.AvionId);
        }
    }
}
