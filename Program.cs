using System.Text;
using agendamentosmanager_api.Models;
using agendamentosmanager_api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Database App

builder.Services.AddDbContextPool<AgendamentobotContext>((serviceProvider, options) =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

string MyAllowSpecificOrigins = "_myAllowspecificOrigins";
builder.Services.AddCors(options => {
    options.AddPolicy(MyAllowSpecificOrigins, builder => 
    {
        builder.WithOrigins("https://medscan-web.fly.dev", "http://localhost:3000");
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
    });
});


builder.Services.AddControllers();

var key = Encoding.ASCII.GetBytes(agendamentosmanager_api.Settings.Secret);

builder.Services.AddAuthorization(x => 
{
    x.AddPolicy("AdminPolicy", p => p.RequireAuthenticatedUser().RequireClaim("Perfil", "Admin"));
});

builder.Services.AddAuthentication(x =>
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
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<AgendamentosService>();
builder.Services.AddScoped<ServicosService>();
builder.Services.AddScoped<HorariosService>();
builder.Services.AddScoped<UsuariosService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
