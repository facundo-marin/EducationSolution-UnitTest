using Education.Domain;
using Microsoft.EntityFrameworkCore;

namespace Education.Persistence
{
    public class EducationDbContext : DbContext
    {
        public EducationDbContext(){}
        //Esto se hace para que la application pueda mandar las configuraciones al dbcontext 
        // se pone base pra que setee las opciones  al padre.
        public EducationDbContext(DbContextOptions<EducationDbContext> options) : base(options) { }


        //clases que se van a convertir en un tipo entidad.
        public DbSet<Curso> Cursos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer(
                    "Server=localhost;database=EducationTestUnit;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True");
            }
        }


        //Agrego datos.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //cantidad de decimales para precio puede agarara 14 numeros enteros y 2 despues de la coma.
            modelBuilder.Entity<Curso>()
                .Property(x => x.Precio)
                .HasPrecision(14, 2);

            modelBuilder.Entity<Curso>().HasData(
                new Curso
                {
                    CursoId = Guid.NewGuid(),
                    Descripcion = "Curso de C# basico",
                    Titulo = "C# desde cero hasta avanzado.",
                    FechaCreacion = DateTime.Now,
                    FechaPublicacion = DateTime.Now.AddYears(2),
                    Precio = 56
                }
            );

            modelBuilder.Entity<Curso>().HasData(
                new Curso
                {
                    CursoId = Guid.NewGuid(),
                    Descripcion = "Curso de Java",
                    Titulo = "Master en Java Spring desde las raices",
                    FechaCreacion = DateTime.Now,
                    FechaPublicacion = DateTime.Now.AddYears(2),
                    Precio = 25
                }
            );

            modelBuilder.Entity<Curso>().HasData(
                new Curso
                {
                    CursoId = Guid.NewGuid(),
                    Descripcion = "Curso de Unit Test para NET Core",
                    Titulo = "Master en Unit Test con CQRS",
                    FechaCreacion = DateTime.Now,
                    FechaPublicacion = DateTime.Now.AddYears(2),
                    Precio = 1000
                }
            );
        }
    }
}
