
using Arizona.APIs.Errors;
using Arizona.APIs.Extensions;
using Arizona.APIs.Helpers;
using Arizona.APIs.Middlewares;
using Arizona.Core.Entities;
using Arizona.Core.Entities.Identity;
using Arizona.Core.Repositories.Contract;
using Arizona.Infrastructure;
using Arizona.Infrastructure.Data;
using Arizona.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Text;

namespace Arizona.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            #region Add Application Services


            // Add services to the container.

            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            builder.Services.AddSwaggerServices();

            //Add Project Reference to Repository Layer


            builder.Services.AddContextsServices(builder.Configuration);

            builder.Services.AddAuthServices(builder.Configuration);

            builder.Services.AddApplicationServices();


            #endregion


            var app = builder.Build();


            #region ASK CLR to create Object from DbContext Explicitly

            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var _dbContext = services.GetRequiredService<StoreContext>();
            var _identityDbContext = services.GetRequiredService<ApplicationIdentityDbContext>();

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();


            try
            {
                await _dbContext.Database.MigrateAsync();

                await StoreContextSeed.SeedAsync(_dbContext); 

                await _identityDbContext.Database.MigrateAsync();

                var _userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                await ApplicationIdentityDataSeed.SeedUsersAsync(_userManager);
            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An Error has been occured during apply the Migration");
            }

            #endregion

            #region Add Application Middlewares

            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlewares();
            }

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.MapControllers();

            app.Run(); 

            #endregion
        }
    }
}
