using InternetServiceBack.Models;
using InternetServiceBack.Filters;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DatabaseInternetServiceContext>(options =>
    options.UseSqlServer((new ConfigurationBuilder()).AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings").GetValue<string>("DefaultConnection")));
builder.Services.AddScoped<UserSessionFilter>();
builder.Services.AddControllers();
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
app.UseCors(builder =>
    builder.WithOrigins("http://localhost:4200")
           .AllowAnyHeader()
           .AllowAnyMethod()
);

app.UseAuthorization();

app.MapControllers();

app.Run();
