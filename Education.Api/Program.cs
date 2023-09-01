using System.Reflection;
using Education.Application.Cursos;
using Education.Persistence;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Agregamos el context
builder.Services.AddDbContext<EducationDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//Agregamos el mediatR
builder.Services.AddMediatR(typeof(GetCursoQuery.GetCursoQueryHandler).Assembly);


builder.Services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<CreateCursoCommand>());

builder.Services.AddAutoMapper(typeof(GetCursoQuery.GetCursoQueryHandler));

builder.Services.AddCors(o => o.AddPolicy("corsApp", builder =>
{
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyOrigin();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseCors("corsApp"); 

app.UseAuthorization();

app.MapControllers();

app.Run();
