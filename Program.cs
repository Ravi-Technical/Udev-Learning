using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Udemy_Backend.Database;
using Udemy_Backend.Interface;
using Udemy_Backend.Interface.Admin;
using Udemy_Backend.Interface.course;
using Udemy_Backend.Interface.UI;
using Udemy_Backend.Services;
using Udemy_Backend.Services.Admin;
using Udemy_Backend.Services.Course;
using Udemy_Backend.Services.UI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MyDbContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// JSON Serialization
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// Token Key Setting 
var key = builder.Configuration["Jwt:Key"];
var issuer = builder.Configuration["Jwt:Issuer"]; 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!)),
            RoleClaimType = ClaimTypes.Role,
            NameClaimType = ClaimTypes.Name,
        };

        // Unauthorized Error Handling
        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                context.HandleResponse();
                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                return context.Response.WriteAsJsonAsync(new
                {
                    status = 401,
                    message = "Unauthorized access. Please provide a valid token."
                });
            }
        };

    });

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


// Added Services 
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IEmailService, MailService>();

// Admin Service Interface Injected
builder.Services.AddScoped<ICategory, CategoryService>();
builder.Services.AddScoped<ICourse, CourseServices>();

// User Service Interface Injected 
builder.Services.AddScoped<User_ICategory, UIService>();
builder.Services.AddScoped<User_ICourse, UICourseService>();
builder.Services.AddScoped<IHomeSlider, HomeSlider_Service>();

// Admin Country State Interface Injected
builder.Services.AddScoped<IAdminInterfaces, AdminCountryStateService>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("UserPolicy", policy => policy.RequireRole("User"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowAll");

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
