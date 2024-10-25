using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;
using Uzd2;
using Uzd2.Datatypes;
using Uzd2.Policies;
using Uzd2.Services;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddDbContext<Uzd2Context>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IMajaService, MajaService>();
builder.Services.AddScoped<IDzivService, DzivService>();
builder.Services.AddScoped<IIedzService, IedzService>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<ITokenSerivce, TokenService>();
builder.Services.AddScoped<IAuthorizationHandler, ApartmentNumberHandler>();
builder.Services.AddScoped<IAuthorizationHandler, ApartmentNumberHandlerHouse>();
builder.Services.AddScoped<IAuthorizationHandler, ApartmentNumberHandlerResident>();
builder.Services.AddScoped<IAuthorizationHandler, ApartmentNumberHandlerUpdate>();


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<Uzd2Context>()
    .AddDefaultTokenProviders();



builder.Services.AddControllers();
builder.Services.AddAuthentication(options =>
{
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CorrectApartment", policy => policy.Requirements.Add(new ApartmentNumberRequirment()));
    options.AddPolicy("CorrectApartmentHouse", policy => policy.Requirements.Add(new ApartmentNumberRequirmentHouse()));
    options.AddPolicy("CorrectApartmentResident", policy => policy.Requirements.Add(new ApartmentNumberRequirmentResident()));
    options.AddPolicy("CorrectResidentUpdate", policy => policy.Requirements.Add(new ApartmentNumberRequirmentResidentUpdate()));
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
    options.AddPolicy("AllowLocalHost", options => { options.AllowAnyMethod(); options.AllowAnyHeader(); });
}
);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddScoped<ISecurityStampValidator, SecurityStampValidator<ApplicationUser>>();

builder.Services.AddSwaggerGen(setup =>
{
    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });

});
var app = builder.Build();

var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

await SeedData.SeedRoles(services);
app.UseCors(options => { options.AllowAnyOrigin(); options.AllowAnyHeader();options.AllowAnyMethod(); });
app.UseCors("AllowLocalHost");


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
