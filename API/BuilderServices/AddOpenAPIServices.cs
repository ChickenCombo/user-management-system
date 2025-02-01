using API.Transformers;
using Scalar.AspNetCore;

namespace API.BuilderServices
{
    public static class OpenAPIServiceExtensions
    {
        public static IServiceCollection AddOpenAPIServices(this IServiceCollection services)
        {
            services.AddOpenApi(options =>
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

            return services;
        }

        public static void AddOpenAPIScalarReference(this IEndpointRouteBuilder app)
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
    }
}

