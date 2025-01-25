using API.Transformers;
using Core.Interfaces;
using Infrastructure;
using Infrastructure.Repositories;
using Newtonsoft.Json;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});
builder.Services.AddOpenApi(options =>
{
    options.AddSchemaTransformer<DummyDataTransformer>();
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
    options.AddDocumentTransformer((document, context, _) =>
    {
        document.Info = new()
        {
            Title = "User Management API",
            Version = "v1",
            Description = """
                Dummy User Management API using .NET 9 with Clean Architecture design pattern,
                and Scalar for OpenAPI documentation 
                """,
        };
        return Task.CompletedTask;
    });
});
builder.Services.AddAuthentication().AddJwtBearer();

builder.Services.AddScoped<IUserRepository, UserRepository>();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .WithPreferredScheme("Bearer");
    });
    app.MapGet("/", () => Results.Redirect("/scalar/v1"))
        .ExcludeFromDescription();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
