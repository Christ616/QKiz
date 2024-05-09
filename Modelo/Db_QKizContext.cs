using Microsoft.EntityFrameworkCore;

namespace QKiz.Modelo
{
    public class Db_QKizContext : DbContext
    {
        public Db_QKizContext(DbContextOptions<Db_QKizContext> options) : base(options)
        {
        }

        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }

        public class Empleado
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string ApellidoPaterno { get; set; }
            public string ApellidoMaterno { get; set; }
            public string Sexo { get; set; }
            public string NumeroSeguroSocial { get; set; }
            public string RFC { get; set; }
            public string Telefono { get; set; }
            public string Email { get; set; }
            public Direccion Direccion { get; set; } // Relación con la entidad Direccion
                                                     // Otras propiedades del Empleado según tus necesidades
        }

        public class Direccion
        {
            public int Id { get; set; }
            public string Calle { get; set; }
            public string NumeroExterior { get; set; }
            public string NumeroInterior { get; set; }
            public string CodigoPostal { get; set; }
            public string Colonia { get; set; }
            public string Ciudad { get; set; }
            public string Estado { get; set; }
            // Otras propiedades de la dirección según tus necesidades
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuraciones adicionales de las entidades, como relaciones, índices, etc.
            // Por ejemplo:
            // modelBuilder.Entity<Empleado>().HasIndex(e => e.Email).IsUnique();
        }
    }
}
