using profile.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using profile.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(options =>
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Cервис пользователей Student Dashboard",
        Description = "Обработка запросов о пользователях Student Dashboard",
        License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://www.mit.edu/~amini/LICENSE.md") }
    })
);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddDbContext<DataContext>(
    options => options.UseNpgsql(DatabaseData.Connection)
);
builder.Services.AddScoped<ProfileService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
    app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
