using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ToDo.App.Domain.Entities;
using ToDo.App.Infrastructure.Options;
using ToDo.App.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<JwtConfig>(
    builder.Configuration.GetSection(nameof(JwtConfig)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt=>opt.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    var jwtOptions = builder.Configuration.GetSection(nameof(JwtConfig)).Get<JwtConfig>();

    opt.RequireHttpsMetadata = false;
    opt.SaveToken = true;

    opt.TokenValidationParameters = new TokenValidationParameters()
    {
        RequireSignedTokens = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
        TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.EncryptKey)),
        ValidateIssuer = true,
        ValidIssuer = jwtOptions.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtOptions.Audience
    };

    opt.Events = new JwtBearerEvents()
    {
        OnChallenge = ctx =>
        {
            if (ctx.AuthenticateFailure != null)
                throw new UnauthorizedAccessException("مشکلی در احرازهویت شما رخ داده است");

            throw new UnauthorizedAccessException("شما احرازهویت نشده اید");
        }
    };
});


builder.Services.AddIdentity<User, Role>(opt =>
{
    opt.SignIn.RequireConfirmedPhoneNumber = false;
    opt.SignIn.RequireConfirmedEmail = false;
    opt.SignIn.RequireConfirmedAccount = true;

    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<AppDbContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
