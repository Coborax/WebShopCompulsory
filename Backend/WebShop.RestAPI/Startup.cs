using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebShop.Core.IServices;
using WebShop.Core.Models;
using WebShop.Core.Services;
using WebShop.Domain;
using WebShop.Domain.IRepositories;
using WebShop.Domain.Services;
using WebShop.Infrastructure.Auth.JWT.Services;
using Webshop.Infrastructure.DB.EFCore;
using Webshop.Infrastructure.DB.EFCore.Entities;
using Webshop.Infrastructure.DB.EFCore.Helpers;
using Webshop.Infrastructure.DB.EFCore.Repositories;

namespace WebShop.RestAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebShop.RestAPI", Version = "v1" });
            });

            services.AddCors(opt =>
            {
                opt.AddPolicy("dev-cors", builder =>
                {
                    builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins("http://localhost:4200");
                });
            });
            
            // Generate secret key
            Byte[] secretBytes = new byte[40];
            Random rand = new Random();
            rand.NextBytes(secretBytes);
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretBytes),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdmin", policy => policy.RequireRole("Administrator"));
            });

            services.AddDbContext<WebShopContext>(opt =>
            {
                opt.UseLazyLoadingProxies();
                opt.UseSqlite("Data Source=WebShop.db");
            });

            services.AddSingleton<IModelConverter<Product, ProductEntity>, ProductModelConverter>();
            services.AddSingleton<IModelConverter<User, UserEntity>, UserModelConverter>();

            services.AddScoped<IRepo<Product>, EFCoreRepo<Product, ProductEntity>>();
            services.AddScoped<IUserRepo, EFCoreUserRepo>();
            services.AddScoped<IUnitOfWork, EFCoreUnitOfWork>();

            services.AddScoped<IProductService, ProductService>();

            services.AddSingleton<IAuthService>(new JWTAuthService(secretBytes));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebShop.RestAPI v1"));
                app.UseCors("dev-cors");

                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var ctx = scope.ServiceProvider.GetService<WebShopContext>();
                    var uow = scope.ServiceProvider.GetService<IUnitOfWork>();
                    var auth = scope.ServiceProvider.GetService<IAuthService>();

                    ctx.Database.EnsureDeleted();
                    ctx.Database.EnsureCreated();
                    
                    
                    uow.Users.Create(new User
                    {
                        Username = "admin", 
                        Password = auth.HashPassword("admin"),
                        Role = new Role { Id = 2 }
                    });
                    uow.Complete();
                }
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}