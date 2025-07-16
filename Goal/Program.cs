
using System.Text;
using Goal.API.Controllers.Mapping;
using Goal.Core.Helpers;
using Goal.Core.Interfaces.Services;
using Goal.Core.Models;
using Goal.Core.UnitOfWork;
using Goal.Data;
using Goal.Data.Services;
using Goal.Data.Services.Jobs;
using Goal.Data.UnitOfWork;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Goal.API
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

            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Goals"),
             b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName))
                );
            builder.Services.AddScoped<IProductServices, ProductServices>();
            builder.Services.AddScoped<IDiscountServices, DiscountServices>();
            builder.Services.AddScoped<ICategoryServices, CategoryServices>();
            builder.Services.AddScoped<IBrandServices, BrandServices>();
            builder.Services.AddScoped<IImageServices, ImageServices>();
            builder.Services.AddScoped<IAuthServices, AuthServices>();
            builder.Services.AddScoped<EmailService>();
            builder.Services.Configure<CloudinarySettings>(
                builder.Configuration.GetSection("CloudinarySettings"));

            builder.Services.Configure<JWT>(
                builder.Configuration.GetSection("JWT"));


            builder.Services.AddIdentity<Customer, Role>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(MappingProfile));
            builder.Services.AddHangfire(x => x.UseSqlServerStorage(builder.Configuration.GetConnectionString("Goals")));
            builder.Services.AddHangfireServer();
            builder.Services.Configure<IdentityOptions>(op =>
            {
                op.SignIn.RequireConfirmedEmail = true;
            });


            builder.Services.AddAuthentication(optiens =>
            {
                optiens.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                optiens.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(o =>
              {
                  o.RequireHttpsMetadata = false;
                  o.SaveToken = false;
                  o.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidIssuer = builder.Configuration["JWT:Issuer"],
                      ValidAudience = builder.Configuration["JWT:Audience"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
                  };
              }


          );



            var app = builder.Build();

            app.UseHangfireDashboard();
            RecurringJob.AddOrUpdate<DiscountService>(
                "update-discount-job",
                service => service.UpdateDiscounts(),
                "*/1 * * * *"
                );

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {

                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}