

using Library.Models;
using Microsoft.EntityFrameworkCore;
using ToDoAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlExpressConnection")));

var app = builder.Build();


app.UseHttpsRedirection();

app.MapGet("api/todo", async (AppDbContext context) =>
{
    var items = await context.ToDos.ToListAsync();
    return Results.Ok(items);
});

app.MapPost("api/todo", async (AppDbContext context, Todo todo) =>
{
    await context.ToDos.AddAsync(todo);
    await context.SaveChangesAsync();
    return Results.Created($"api/todo/{todo.Id}", todo);
});


app.MapPut("api/todo/{id}", async (AppDbContext context, int id, Todo todo) =>
{
    var result = await context.ToDos.FirstOrDefaultAsync(x => x.Id == id);
    if(result == null)
    {
        return Results.NotFound();
    }
    result.Title = todo.Title;
    result.Description = todo.Description;
    await context.SaveChangesAsync();
    return Results.NoContent();
});

app.MapPut("api/todo/{id}", async (AppDbContext context, int id) =>
{
    var result = await context.ToDos.FirstOrDefaultAsync(x => x.Id == id);
    if (result == null)
    {
        return Results.NotFound();
    }

    context.ToDos.Remove(result);
    await context.SaveChangesAsync();
    return Results.NoContent();
});



app.Run();

