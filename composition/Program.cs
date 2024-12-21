using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Серверная часть личного кабинета студента Student Dashboard",
        Description = "Доступ к авторизации, курсам, сообщения, платежам, персональным данным и расписанию",
        License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://www.mit.edu/~amini/LICENSE.md") }
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
