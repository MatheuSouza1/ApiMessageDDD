
using AutoMapper;
using Domain.Interfaces.Generics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebApi.models;
using WebApi.Token;
using Domain.Interfaces;
using Domain.Interfaces.InterfaceServices;
using Domain.Services;
using Entities.Entities;
using Infraestructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Infraestructure.Repository.Repositories;
using Infraestructure.Repository.Generics;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // ConfigServices
            builder.Services.AddDbContext<ContextBase>(options =>
              options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ContextBase>();

            //Interface and Repository Services
            builder.Services.AddSingleton(typeof(IGeneric<>), typeof(GenericRepository<>));
            builder.Services.AddSingleton<IMessage, MessageRepository>();

            //Domain Services
            builder.Services.AddSingleton<IServiceMessage, ServiceMessage>();


            // JWT
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                  .AddJwtBearer(option =>
                  {
                      option.TokenValidationParameters = new TokenValidationParameters
                      {
                          ValidateIssuer = false,
                          ValidateAudience = false,
                          ValidateLifetime = true,
                          ValidateIssuerSigningKey = true,

                          ValidIssuer = "Api.Security.Bearer",
                          ValidAudience = "Api.Security.Bearer",
                          IssuerSigningKey = JwtSecurityKey.CreateToken("Secret_Key-12345678")
                      };

                      option.Events = new JwtBearerEvents
                      {
                          OnAuthenticationFailed = context =>
                          {
                              Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                              return Task.CompletedTask;
                          },
                          OnTokenValidated = context =>
                          {
                              Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                              return Task.CompletedTask;
                          }
                      };
                  });


            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<MessageViewModel, Message>();
                cfg.CreateMap<Message, MessageViewModel>();
            });

            IMapper mapper = config.CreateMapper();
            builder.Services.AddSingleton(mapper);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.UseSwaggerUI();

            app.Run();
        }
    }
}