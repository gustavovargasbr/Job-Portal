using JOB_PORTAL.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext and Identity
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Add Authentication and Authorization
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

// Register controllers
builder.Services.AddControllers();

// Add CORS configuration to allow requests from localhost:7135
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("https://localhost:7135")  // Allow requests from this origin
               .AllowAnyMethod()                      // Allow any HTTP method (GET, POST, DELETE, etc.)
               .AllowAnyHeader();                     // Allow any headers (Authorization, etc.)
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use CORS middleware before authentication and authorization
app.UseCors("AllowSpecificOrigin");

// Add middleware for authentication
app.UseAuthentication();
app.UseAuthorization();

// Map controller routes
app.MapControllers();

app.Run();
