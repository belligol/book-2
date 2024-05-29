using BookStore_BL.Interfaces;
using BookStore_BL.Services;
using BookStore_DL.Interfaces;
using BookStore_DL.Repositories;
using BookStore.HealthChecks;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using BookStore_Models.Configurations;
using BookStore_DL.Repositories.Mongo;
using BookStore_Models.Configurations.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using AspNetCore.Identity.MongoDbCore.Models;
using BookStore_Models.Models;
using Microsoft.AspNetCore.Identity;
using Serilog;
using BookStore_DL.Kafka;
using BookStore_BL.BackgroundJobs;

namespace BookstoreApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            Log.Information("Starting web application");

            var jwtSettings = new JwtSettings();

            builder.Configuration.Bind(nameof(JwtSettings), jwtSettings);
            builder.Services.AddSingleton(jwtSettings);

            builder.Host.UseSerilog();

            builder.Services.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(x =>
                {
                    x.SaveToken = true;
                    x.TokenValidationParameters = new()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                        ValidateIssuer = false,
                        ValidAudience = null,
                        ValidateLifetime = true
                    };
                });
            builder.Services.AddSingleton<ProducerOrigin<Guid, Book>>();
            builder.Services.AddHostedService<BookConsumerService>();

            builder.Services.Configure<MongoConfiguration>(
               builder.Configuration.GetSection(
                   nameof(MongoConfiguration)));

            // Add services to the container.
            builder.Services.AddSingleton<IAuthorRepository, AuthorMongoRepository>();
            builder.Services.AddSingleton<IBookRepository, BookMongoRepository>();

            builder.Services.AddSingleton<IAuthorService, AuthorService>();
            builder.Services.AddSingleton<IBookService, BookService>();
            builder.Services.AddSingleton<ILibraryService, LibraryService>();

            builder.Services.AddScoped<IIdentityService, IdentityService>();
            builder.Services.AddSingleton<IProduceBookService, ProduceBookService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(x =>
            {
                var security = new Dictionary<string, IEnumerable<string>>()
                {
                    {"Bearer", Array.Empty<string>() }
                };
                OpenApiSecurityScheme securityDefinition = new()
                {
                    Name = "Bearer",
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Description = "Specify authorization token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http
                };
                x.AddSecurityDefinition("jwt_auth", securityDefinition);
                x.AddSecurityRequirement(new OpenApiSecurityRequirement()
                { 
                    {securityDefinition, new string[] {} }
                });

            });

            var config = builder.Configuration.GetSection(nameof(MongoConfiguration));
            var mongoCfg = builder.Configuration.GetSection(nameof(MongoConfiguration))
                .Get<MongoConfiguration>();

            builder.Services.AddIdentity<User, MongoIdentityRole>()
                .AddMongoDbStores<User, MongoIdentityRole, Guid>(mongoCfg.ConnectionString, mongoCfg.DatabaseName)
                .AddSignInManager()
                .AddDefaultTokenProviders();

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining(typeof(Program));
            builder.Services.AddHealthChecks().AddCheck<CustomHealthCheck>(nameof(CustomHealthCheck));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
