using RecipeShareAPI.Extensions;
using RecipeShareAPI.Models.Settings;

var builder = WebApplication.CreateBuilder(args);

// Centralized service registration
builder.Services.AddRecipeShareServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use the CORS policy
app.UseCors("LocalhostOnly");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
