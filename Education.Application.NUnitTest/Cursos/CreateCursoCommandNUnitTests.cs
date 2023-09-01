using AutoFixture;
using AutoMapper;
using Education.Application.Helper;
using Education.Domain;
using Education.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Education.Application.Cursos
{
    [TestFixture]
    public class CreateCursoCommandNUnitTests
    {
        private CreateCursoCommand.CreateCursoCommandHandler handlerCursoCreate;

        [SetUp]
        public void Setup()
        {
            //Creamos los datos de prueba para Cursos
            var fixture = new Fixture();
            var cursoRecords = fixture.CreateMany<Curso>().ToList();

            //agregamos un curso con id nulo
            cursoRecords.Add(fixture.Build<Curso>()
                .With(tr => tr.CursoId, Guid.Empty)
                .Create()
            );


            //Con esto creamos la db en memoria y copiamos la configuracion
            var options = new DbContextOptionsBuilder<EducationDbContext>()
                .UseInMemoryDatabase(databaseName: $"EducationDbContext-{Guid.NewGuid()}")
                .Options;

            //creamos el dbContext Fake
            var educationDbContextFake = new EducationDbContext(options);

            //Ahora debemos agregar los datos a nuestra bd porque se crea vacia.
            educationDbContextFake.AddRange(cursoRecords);
            educationDbContextFake.SaveChanges();

            //instanciamos el mapping
            var mapCongif = new MapperConfiguration(cfg => cfg.AddProfile(new MappingTest()));

            var mapper = mapCongif.CreateMapper();

            //Instanciamos GetQueryHabdler
            handlerCursoCreate = new CreateCursoCommand.CreateCursoCommandHandler(educationDbContextFake);
        }

        [Test]
        public async Task CreateCursoCommand_InputCurso_ReturnsNumber()
        {
            var request = new CreateCursoCommand.CreateCursoCommandRequest();
            request.FechaPublicacion = DateTime.UtcNow.AddDays(59);
            request.Titulo = "Libro de pruebas Automaticas NET";
            request.Descripcion = "Aprende a crear unit test desde cero";
            request.Precio = 99;

            var resultados = await handlerCursoCreate.Handle(request, new CancellationToken());

            Assert.That(resultados, Is.EqualTo(Unit.Value));

        }
    }
}
