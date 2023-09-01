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
    public class GetCursoByIdQueryNUnitTests
    {
        private GetCursoByIdQuery.GetCursoByIdQueryHandler handlerByIdCurso;
        private Guid cursoIdTest;

        [SetUp]
        public void Setup()
        {
            cursoIdTest = new Guid("2ce24c4d-df93-4620-ba47-c8065633775f");

            //Creamos los datos de prueba para Cursos
            var fixture = new Fixture();
            var cursoRecords = fixture.CreateMany<Curso>().ToList();

            //agregamos un curso con id nulo
            cursoRecords.Add(fixture.Build<Curso>()
                .With(tr => tr.CursoId, cursoIdTest)
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
            handlerByIdCurso = new GetCursoByIdQuery.GetCursoByIdQueryHandler(educationDbContextFake, mapper);
        }

        [Test]
        public async Task GetCurosByIdQueryHandler_InputCursoId_ReturnsNotNull()
        {
            var request = new GetCursoByIdQuery.GetCursoByIdQueryRequest
            {
                Id = cursoIdTest
            };
            var resultados = await handlerByIdCurso.Handle(request, new CancellationToken());

            Assert.IsNotNull(resultados);

        }
    }
}
