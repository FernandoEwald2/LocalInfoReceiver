using Api.Data;
using Api.Exceptions;
using Api.Services;
using Api.Services.Interfaces;
using Api.Util;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        // Configuração da string de conexão com variáveis de ambiente
        var config = Configuration.GetConnectionString("DefaultConnection");

        var dbUser = Environment.GetEnvironmentVariable("DB_USER");
        var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");       

        if (!string.IsNullOrEmpty(dbUser) && !string.IsNullOrEmpty(dbPassword))
        {
            config = config.Replace("{DB_USER}", dbUser)
                           .Replace("{DB_PASSWORD}", dbPassword);
        }

        // Configuração do DbContext para PostgreSQL
        services.AddDbContext<LocalDbContext>(options =>
            options.UseNpgsql(config)
                   .EnableSensitiveDataLogging(false));

        // Registro de serviços
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IAuthService, AuthService>();
        
        // Adiciona autenticação JWT
        var jwtKey = Environment.GetEnvironmentVariable("JWT_SECRET");
        if (string.IsNullOrEmpty(jwtKey))
        {
            throw new InvalidOperationException("JWT_SECRET environment variable is not set.");
        }
        var jwtSecretKey = Encoding.ASCII.GetBytes(jwtKey);

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(jwtSecretKey),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            x.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var token = context.Request.Headers["Authorization"].FirstOrDefault();
                    if (!string.IsNullOrEmpty(token))
                    {
                        context.Token = token; // pega o token direto, sem precisar do prefixo Bearer
                    }
                    return Task.CompletedTask;
                }
            };
        });

        services.AddAuthorization();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                builder => builder.WithOrigins("http://localhost:4200")
                                  .AllowAnyMethod()
                                  .AllowAnyHeader()
                                  .AllowCredentials());
        });

        // Configuração dos controladores e JSON
        services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

        // Configuração do Swagger
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api LocalInfo", Version = "v1" });
            c.AddServer(new OpenApiServer { Url = "/api" });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Bearer",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Localinfo v1");
                c.RoutePrefix = "swagger"; // Tornar o Swagger acessível na raiz da API
            });
        }

        app.UseRouting();
        app.UseCors("AllowSpecificOrigin");

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseMiddleware<ExceptionMiddleware>();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}

