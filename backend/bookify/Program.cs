using bookifyWEBApi.SignalR;
using DataAcces;
using DataAccess.Repos;
using Interfaces.IRepos;
using Logic.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add environment variables
builder.Configuration.AddEnvironmentVariables();

// Configureren van de databasecontext met User Secrets
builder.Services.AddDbContext<BookifyContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Bookify")));


// Register repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICollectionRepository, CollectionRepository>();

// Register services
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<CollectionService>();

// Register JwtService
builder.Services.AddScoped<JwtService>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    var secretKey = config["JwtSettings:SecretKey"]!;
    var issuer = config["JwtSettings:Issuer"]!;
    var audience = config["JwtSettings:Audience"]!;
    return new JwtService(secretKey, issuer, audience);
});

// Configureren van JWT-authenticatie
var jwtKey = builder.Configuration["JwtSettings:SecretKey"]!;
builder.Services.AddAuthentication(options =>
{
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
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();

// Configure CORS to allow all origins, methods, and headers
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigin", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Middleware-configuratie
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

// Gebruik het CORS-beleid
app.UseCors("AllowAnyOrigin");

// Voeg authenticatiemiddleware toe v��r de authorizatiemiddleware
app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

// Configureer het gebruik van statische bestanden
app.UseDefaultFiles(); // Zorgt ervoor dat index.html standaard wordt geserveerd
app.UseStaticFiles();  // Bedient statische bestanden uit de wwwroot-map

// Voeg fallback-middleware toe om onbekende routes naar index.html te leiden
app.MapFallbackToFile("index.html"); // Zorg ervoor dat index.html in de wwwroot-map staat



app.MapHub<UserCountHub>("/userCountHub");
app.UseWebSockets();

app.MapGet("/", () => "huts");

await app.RunAsync();
