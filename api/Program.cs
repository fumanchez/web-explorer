using System.Text.Json.Serialization;
using Microsoft.AspNetCore.StaticFiles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IContentTypeProvider, FileExtensionContentTypeProvider>();

builder.Services.Configure<RouteOptions>(options => {
    options.LowercaseUrls = true;
});

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors(policy => {
        policy.AllowAnyOrigin();
    });
}

app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.Run();
