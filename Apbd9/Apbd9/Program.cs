using Apbd9.Context;
using Apbd9.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<ITripsService, TripsServiceImpl>();
builder.Services.AddDbContext<MasterContext>();
builder.Services.AddScoped<IClientsService, ClientsServiceImpl>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

