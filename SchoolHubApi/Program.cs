using SchoolHubApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddProblemDetails();

builder.Services.AddDatabase();
builder.Services.AddEmail();
builder.Services.AddValidators();
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddExceptionHandlers();

var app = builder.Build();

app.Services.MigrateDatabase();

app.UseExceptionHandler();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
