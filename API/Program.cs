using API.BuilderServices;
using API.Endpoints;
using Infrastructure;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Newtonsoft.Json;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddOpenAPIServices();
builder.Services.AddCORSServices();
builder.Services.AddErrorHandling();
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.AddOpenAPIScalarReference();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapUserEndpoint();

app.Run();
