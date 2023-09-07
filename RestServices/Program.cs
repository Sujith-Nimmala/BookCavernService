
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using REST.Config;
using RestServices;
using RestServices.Models.Db;
using System.Text;

namespace REST
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            

builder.Services.AddCors(config =>

{

    config.AddPolicy("policy2", configurePolicy =>

    {

        configurePolicy.AllowAnyHeader();

        configurePolicy.AllowAnyMethod();

        configurePolicy.WithOrigins("http://localhost:4200");

    });

});

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Jwt:audience"],
                    ValidIssuer = builder.Configuration["Jwt:issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]!))
                };
            });

            builder.Services.AddAuthorization(config => {
                config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
                config.AddPolicy(Policies.Customer, Policies.CustomerPolicy());
            });

            string connectionstring = "Server=localhost;database=BookStore;trusted_connection=yes;trustservercertificate=true";
            builder.Services.AddDbContext<BookStoreContext>(config => config.UseSqlServer(connectionstring));

            builder.Services.AddTransient(typeof(DataService));
            builder.Services.AddTransient(typeof(SecurityService));
            builder.Services.AddTransient(typeof(AppUserService));
            builder.Services.AddTransient(typeof(BookDetailsService));
            builder.Services.AddTransient(typeof(GetOrdersService));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("policy2");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}