using Microsoft.EntityFrameworkCore;
using Services.Interfaces;
using TrackademiApi.Models.Entities; // Asegúrate de este using
using TrackademiApi.Repository;
using TrackademiApi.Repository.Interfaces;
using TrackademiApi.Services;
using TrackademiApi.Services.Interfaces;
using TrackademiApi.Services.Interfaces.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// agrego la cadena de conexin
builder.Services.AddDbContext<TrackademiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// agrego el repositorio Repositorios genéricos
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// servicios
builder.Services.AddScoped<IEstudiantesService, EstudiantesService>();
builder.Services.AddScoped<IUsuariosService, UsuariosService>();
builder.Services.AddScoped<IPerfilesService, PerfilesService>();
builder.Services.AddScoped<IMateriasService, MateriasService>();
builder.Services.AddScoped<ICalificacionesService, CalificacionesService>();
builder.Services.AddScoped<IControlAsistenciaService, ControlAsistenciaService>();
builder.Services.AddScoped<IGenericService<PeriodosDto>, GenericService<Periodos, PeriodosDto>>();



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var corsPolicyName = "AllowAngularClient";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName, policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var app = builder.Build();
app.UseCors(corsPolicyName);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
