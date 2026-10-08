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
builder.Services.AddJwt();

var app = builder.Build();

app.Services.MigrateDatabase();

app.UseExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
