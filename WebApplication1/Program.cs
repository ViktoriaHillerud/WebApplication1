
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            builder.Services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("CatDb"));

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

            app.UseAuthorization();

            //Get one cat
            app.MapGet("/cats", (ApplicationContext _context) =>
            {
                List<Cat> cat = _context.Cats.ToList();
                return cat;
            });

            app.MapPost("/cats", (CatDto newCat, ApplicationContext _context) =>
            {
                Cat cat = new Cat()
                {
                    Name = newCat.Name,
                    Age = newCat.Age,
                    Race = newCat.Race,
                    FavouriteTreat = newCat.FavouriteTreat,
                    HasOwner = newCat.HasOwner,
                };
                try
                {
                    _context.Add(cat);
                    _context.SaveChanges();
                }
                 catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }

                return Results.Created("", new { message = $"Cat {cat.Name} successfully saved!" });
            });

            app.MapPut("/cats/updateName", (UpdateCatDto UpdateDto, ApplicationContext _context) =>
            {
                try
                {
                    Cat? cat = _context.Cats.SingleOrDefault(c => c.Id == UpdateDto.Id);

                    if(cat == null)
                    {
                        return Results.BadRequest("No cat with i");
                    }

                    cat.Name = UpdateDto.NewName;
                    _context.Update(cat);
                    _context.SaveChanges();

                } catch
                {
                    throw new Exception();
                }

                return Results.Ok();
            });

            app.MapDelete("/cats/{id:int}", (int id, ApplicationContext _context) =>
            {
                try
                {
                    Cat? cat = _context.Cats.SingleOrDefault(c => c.Id == id);

                    if (cat == null)
                    {
                        return Results.BadRequest("No cat with the specified ID.");
                    }

                    _context.Remove(cat);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return Results.Problem(ex.Message);
                }

                return Results.Ok(new { message = $"Cat with ID {id} successfully deleted!" });
            });


            app.Run();
        }
    }
}
