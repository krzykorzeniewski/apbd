using Apbd4MinimalApi.model;
using Apbd4MinimalApi.repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAnimalsRepository, AnimalsRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("animals", (IAnimalsRepository animalsRepository) =>
{
    return Results.Ok(animalsRepository.GetAll());
});

app.MapGet("animals/{id}", (IAnimalsRepository animalsRepository, int id) =>
{
    var animal = animalsRepository.GetById(id);

    if (animal == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(animal);
});

app.MapDelete("animals/{id}", (IAnimalsRepository animalsRepository, int id) =>
{
    var animalToDelete = animalsRepository.RemoveById(id);

    if (animalToDelete == null)
    {
        return Results.NotFound();
    }

    return Results.NoContent();
});

app.MapPost("animals", (IAnimalsRepository animalsRepository, Animal animal) =>
{
    animalsRepository.Add(animal);
    return Results.Created($"animals/{animal.Id}", animal);
});

app.MapPut("animals/{id}", (IAnimalsRepository animalsRepository, int id, Animal animal) =>
{
    var removedAnimal = animalsRepository.RemoveById(id);

    if (removedAnimal == null)
    {
        return Results.NotFound();
    }
        
    animalsRepository.Add(animal);
        
    return Results.NoContent(); //czy created???
});

app.Run();