using Microsoft.EntityFrameworkCore;
using ValetCodingExercise.Authorization;
using ValetCodingExercise.Interfaces;
using ValetCodingExercise.Models;
using ValetCodingExercise.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
builder.Services.AddDbContextPool<ValetDbExampleContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ValetDbConnection")));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddAuthentication("MyCookieAuth").AddCookie("MyCookieAuth", options =>
{
    options.Cookie.Name = "MyCookieAuth";
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<BasicAuthorization>();

app.Run();
