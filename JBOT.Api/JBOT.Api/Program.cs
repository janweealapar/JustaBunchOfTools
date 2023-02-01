using JBOT.Application.Common.Interfaces;
using JBOT.Application.Queries.Databases;
using JBOT.Infrastructure;
using JBOT.Infrastructure.Persistence;
using MediatR;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options=> options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddPersistence(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMediator, Mediator>();

builder.Services.AddMediatR(typeof(GetAllDatabasesQuery));
builder.Services.AddMediatR(typeof(GetTestableObjectsQuery));

builder.Services.AddMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
