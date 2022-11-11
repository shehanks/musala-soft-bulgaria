using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.EntityFrameworkCore;
using MusalaSoft.Transpotation.BusinessServices;
using MusalaSoft.Transpotation.BusinessServices.Contract;
using MusalaSoft.Transpotation.DataAccess;
using MusalaSoft.Transpotation.Domain;
using MusalaSoft.Transpotation.Repository;
using MusalaSoft.Transpotation.Repository.Contract;
using MusalaSoft.Transpotation.WorkerServices;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add a background worker service to log the drone battery history.
builder.Services.AddScoped<IScopedProcessingService, ScopedProcessingService>();
builder.Services.AddSingleton<DroneAuditWorkerService>();
builder.Services.AddHostedService(
    provider => provider.GetRequiredService<DroneAuditWorkerService>());
//builder.Services.AddHostedService<DroneAuditWorkerService>();


// DI - Registering Automapper
builder.Services.AddAutoMapper(typeof(AutoMappingProfiles).Assembly);

// DI - Registering DB Context.
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
});

// DI - Registering data access objects. 
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IDroneDao, DroneDao>();
builder.Services.AddScoped<IMedicationDao, MedicationDao>();
builder.Services.AddScoped<IDroneTripDao, DroneTripDao>();

// DI - Registering business services.
builder.Services.AddScoped<IDroneService, DroneService>();
builder.Services.AddScoped<IMedicationDispatchService, MedicationDispatchService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
