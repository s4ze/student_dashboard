using authorization.Data;
using authorization.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(options =>
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Cервис авторизации Student Dashboard",
        Description = "Обработка аутентификационных и авторизационных запросов Student Dashboard",
        License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://www.mit.edu/~amini/LICENSE.md") }
    })
); builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<DataContext>(
    options => options.UseNpgsql(DatabaseData.Connection)
);
builder.Services.AddScoped<AuthenticationService>();
builder.Services.AddScoped<AuthorizationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
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
