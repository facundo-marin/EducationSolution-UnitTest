using AutoMapper;
using Education.Application.DTO;
using Education.Domain;
using Education.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Education.Application.Cursos
{
    public class GetCursoQuery
    {
        //request
        public class GetCursoQueryRequest : IRequest<List<CursoDTO>>{}

        //el que se encarga de implemertar la logica para realizar
        //el query a la base de datos que retorna la lista de cursos 
        public class GetCursoQueryHandler : IRequestHandler<GetCursoQueryRequest, List<CursoDTO>>
        {
            private readonly EducationDbContext _context;
            private readonly IMapper _mapper;

            public GetCursoQueryHandler(EducationDbContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<CursoDTO>> Handle(GetCursoQueryRequest request, CancellationToken cancellationToken)
            {
                //Aca va la logica.
                var cursos = await _context.Cursos.ToListAsync();
                var cursosDTO = _mapper.Map<List<Curso>, List<CursoDTO>>(cursos);
                return cursosDTO;
            }
        }
    }
}
