using System.Configuration;
using System.Text;
using Business;
using DataAccess;
using Entities.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services
    .Configure<Jwt>(builder.Configuration.GetSection("Jwt"));

ServiceProvider serviceProvider = builder.Services.BuildServiceProvider();

var jwtConfiguration = serviceProvider.GetRequiredService<IOptions<Jwt>>().Value;


builder.Services
    .AddAuthentication()
    .AddJwtBearer(cfr =>
{
    cfr.TokenValidationParameters = new()
    {
        ValidateIssuer = true, //uygulamanın kime ya da hangi siteye ait olduğunun kontrolünü sağlar
        ValidateAudience = true, //uygulamanın kimlere ya da hangi siteye açıldığının kontolü sağlar
        ValidateIssuerSigningKey = true,//verdiğimiz güvenlik anahtarının doğrulanmasını isteyip istemediğimizi sağlar
        ValidateLifetime = true,//üretilen tokenin süreli olup olmayacağını sağlar
        ValidIssuer = jwtConfiguration.Issuer,//valid issuer değeri
        ValidAudience = jwtConfiguration.Audience,//valid audience değeri
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.SecretKey))
    };
});


builder.Services
    .AddAuthorization();

builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddBusiness();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecuritySheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** yourt JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecuritySheme.Reference.Id, jwtSecuritySheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecuritySheme, Array.Empty<string>() }
                });
});



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();