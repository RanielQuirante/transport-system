using QLESS.TransportSystem.Repositories.Implementations;
using QLESS.TransportSystem.Repositories.Interface;
using QLESS.TransportSystem.Services.Implementations;
using QLESS.TransportSystem.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.IgnoreNullValues = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Add(new ServiceDescriptor(typeof(ITransportCardService), typeof(TransportCardService), ServiceLifetime.Scoped));
builder.Services.Add(new ServiceDescriptor(typeof(ITransportCardRepository), typeof(TransportCardRepository), ServiceLifetime.Scoped));
builder.Services.Add(new ServiceDescriptor(typeof(ITransportCardHistoryRepository), typeof(TransportCardHistoryRepository), ServiceLifetime.Scoped));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
