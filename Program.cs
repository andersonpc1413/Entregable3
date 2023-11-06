using Entregable3;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/contenidoitems", async (Contenido contenido, BaseDatos db) =>
{
    db.Contenido.Add(contenido);
    await db.SaveChangesAsync();

    return Results.Created($"/contenidoitems/{contenido.Id}", contenido);
});

app.MapPut("/contenidoitems/{id}", async (int id, Contenido inputContenido, BaseDatos db) =>
{
    var contenido = await db.Contenido.FindAsync(id);

    if (contenido is null) return Results.NotFound();

    contenido.Titulo = inputContenido.Titulo;
    contenido.Resumen = inputContenido.Resumen;
    contenido.Autorld = inputContenido.Autorld;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/contenidoitems/{id}", async (int id, BaseDatos db) =>
{
    if (await db.Contenido.FindAsync(id) is Contenido contenido)
    {
        db.Contenido.Remove(contenido);
        await db.SaveChangesAsync();
        return Results.Ok(contenido);
    }

    return Results.NotFound();
});

app.MapGet("/contenidoitem", async (BaseDatos db) =>
    await db.Contenido.ToListAsync());


app.Run();
