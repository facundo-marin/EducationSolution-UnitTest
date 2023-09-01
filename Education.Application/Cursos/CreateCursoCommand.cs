using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Education.Domain;
using Education.Persistence;
using FluentValidation;
using MediatR;

namespace Education.Application.Cursos
{
    public class CreateCursoCommand
    {
        //aqui creamos lo que nos tiene que mandar el usuario con las propiedades que necesitamos.
        public class CreateCursoCommandRequest : IRequest
        {
            public string Titulo { get; set; }
            public string Descripcion {get; set; }
            public DateTime FechaPublicacion { get; set; }
            public decimal Precio { get; set; }

        }

        //con  esto validamos la clase CreateCursoCommandRequest
        //para que los campos sean requeridos utilizando FluentValidation.
        public class CreateCursoCommandResquestValidation : AbstractValidator<CreateCursoCommandRequest>
        {
            public CreateCursoCommandResquestValidation()
            {
                RuleFor(x => x.Descripcion);
                RuleFor(x => x.Titulo);
            }
        }


        //En este caso no necesitamos pasarle lo que retorno porque esto se base en
        //seperar la escritura de devolver datos. Asi que solo podemos insertar datos.
        public class CreateCursoCommandHandler : IRequestHandler<CreateCursoCommandRequest>
        {
            private readonly EducationDbContext _context;
            public CreateCursoCommandHandler(EducationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(CreateCursoCommandRequest request, CancellationToken cancellationToken)
            {
                var curso = new Curso
                {
                    CursoId = Guid.NewGuid(),
                    Titulo = request.Titulo,
                    Descripcion = request.Descripcion,
                    FechaCreacion = DateTime.UtcNow,
                    FechaPublicacion = request.FechaPublicacion,
                    Precio = request.Precio,
                };

                _context.Add(curso);
                var valor = await _context.SaveChangesAsync(cancellationToken);

                if (valor < 1)
                {
                    throw new Exception("No se puedo insertar el curso");
                }

                return Unit.Value;
            }
        }
    }
}
