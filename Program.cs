
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.EntityFrameworkCore;
using FilmAPI.Models;
namespace FilmAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
           builder.Services.AddDbContext<ApplicaionDbContext>(options =>
           {
               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
           });


            builder.Services.AddControllers();
            builder.Services.AddCors();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            builder.Services.AddTransient<IGenreServices, GenreServices>();
            builder.Services.AddTransient<IMovieServices, MovieServices>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI( c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FilmAPI v1");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseHttpsRedirection();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()); // Allow CORS for all origins, methods, and headers FOR USE ENDPOINT FROM ANOTHER DOMAIN

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
