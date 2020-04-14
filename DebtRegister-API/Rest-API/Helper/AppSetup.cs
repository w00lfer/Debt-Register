using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Rest_API.Mappings;
using Rest_API.Models;
using Rest_API.Repositories;
using Rest_API.Repositories.Interfaces;
using Rest_API.Services;
using Rest_API.Services.Interfaces;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Rest_API.Helper
{
    public static class AppSetup
    {
        public static void ConfigureAppByDefault(this IServiceCollection services, IConfiguration configuration)
        {
            AddDatabaseAndIdentity(services, configuration);
            AddApplicationOptions(services, configuration);
            ConfigureUserPasswordRequirements(services, configuration.GetSection("DevPasswordConfiguration").Get<PasswordSettings>());
            AddAuthenticationWithJWT(services);
            AddDI(services);
            AddSwagger(services);
            services.AddControllers().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>()); // adds mvc and fluent validatio
        }

        private static void AddApplicationOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            services.Configure<PasswordSettings>(configuration.GetSection("DevPasswordConfiguration"));
        }
        private static void AddDatabaseAndIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("IdentityConnection")));
            services.AddDefaultIdentity<User>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
        }
        private static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Color Diary", Version = "v1" });
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Just paste token you got from signin in or signin up",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                c.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement();
                securityRequirement.Add(securitySchema, new[] { "Bearer" });
                c.AddSecurityRequirement(securityRequirement);
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }
        private static void AddDI(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IDebtService, DebtService>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IDebtRepository, DebtRepository>();
            services.AddAutoMapper(typeof(MappingProfile));
        }
        private static void AddAuthenticationWithJWT(this IServiceCollection services) => services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = "http://dotnetdetail.net",
                    ValidIssuer = "http://dotnetdetail.net",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("This is my custom Secret key for authentication"))
                };
            });
        private static void ConfigureUserPasswordRequirements(this IServiceCollection services, PasswordSettings passwordSettings) =>
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = passwordSettings.RequireDigit;
                options.Password.RequireNonAlphanumeric = passwordSettings.RequireNonAlphanumeric;
                options.Password.RequireLowercase = passwordSettings.RequireLowercase;
                options.Password.RequireUppercase = passwordSettings.RequireUppercase;
                options.Password.RequiredLength = passwordSettings.RequiredLength;
            });
    }
}
