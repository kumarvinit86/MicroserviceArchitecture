using EShopping.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
var services = builder.Services;
services.AddSwaggerGen(swag =>
{
    swag.SwaggerDoc("PanoramaApi", new OpenApiInfo { Title = "Panorama Api", Version = "v1" });

    var security = new Dictionary<string, IEnumerable<string>>
                {
                    {"Bearer", new string[] { }},
                };

    swag.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    swag.AddSecurityRequirement(new OpenApiSecurityRequirement()
                                              {
                                                {
                                                  new OpenApiSecurityScheme
                                                  {
                                                    Reference = new OpenApiReference
                                                      {
                                                        Type = ReferenceType.SecurityScheme,
                                                        Id = "Bearer"
                                                      },
                                                    },
                                                    new List<string>()
                                                  }
                                                });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    swag.IncludeXmlComments(xmlPath);

});

services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(xx =>
    {
        xx.RequireHttpsMetadata = false;
        //xx.SaveToken = true;
        xx.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Constants.SecretKey)),
            ValidIssuer = Constants.Issuer,
            ValidAudience = Constants.Issuer
        };
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
