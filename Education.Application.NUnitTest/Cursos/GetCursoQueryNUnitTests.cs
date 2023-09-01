using AutoFixture;
using AutoMapper;
using Education.Application.Helper;
using Education.Domain;
using Education.Persistence;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Education.Application.Cursos
{
    [TestFixture]
    public class GetCursoQueryNUnitTests
    {
        private GetCursoQuery.GetCursoQueryHandler handlerAllCursos;

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
            handlerAllCursos = new GetCursoQuery.GetCursoQueryHandler(educationDbContextFake, mapper);
        }

        [Test]
        public async Task GetCurosQueryHandler_ConsultaCursos_ReturnsTrue()
        {
            //1. emular al context que representa la instancia del EF

            //2.Emular el Mapping Profile.

            //3. Instanciar un objeto de la clase GetCursoQuery.GetCursoQueryHanlder y pasarle
            //como parametros los objetos context y mapping
            //GetCursoQueryHandler(context mapping) => handle

            var request = new GetCursoQuery.GetCursoQueryRequest();
            var resultados = await handlerAllCursos.Handle(request, new System.Threading.CancellationToken());

            Assert.IsNotNull(resultados);

        }
    }
}
