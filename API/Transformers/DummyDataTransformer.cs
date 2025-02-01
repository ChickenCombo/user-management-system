using API.DTOs.User;
using Bogus;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace API.Transformers
{
    internal sealed class DummyDataTransformer : IOpenApiSchemaTransformer
    {
        private static readonly Dictionary<Type, IOpenApiAny> _examples;

        public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
        {
            if (_examples.TryGetValue(context.JsonTypeInfo.Type, out var example))
            {
                schema.Example = example;
            }

            return Task.CompletedTask;
        }

        static DummyDataTransformer()
        {
            _examples = new Dictionary<Type, IOpenApiAny>
            {
                [typeof(CreateUserDto)] = new OpenApiObject
                {
                    ["firstName"] = new OpenApiString(new Faker("en").Person.FirstName),
                    ["middleName"] = new OpenApiString(new Faker("en").Person.LastName),
                    ["lastName"] = new OpenApiString(new Faker("en").Person.LastName),
                    ["email"] = new OpenApiString(new Faker("en").Person.Email),
                    ["role"] = new OpenApiString(new Faker("en").PickRandom(new[] { "User", "Admin" }))
                },
                [typeof(UpdateUserDto)] = new OpenApiObject
                {
                    ["firstName"] = new OpenApiString(new Faker("en").Person.FirstName),
                    ["middleName"] = new OpenApiString(new Faker("en").Person.LastName),
                    ["lastName"] = new OpenApiString(new Faker("en").Person.LastName),
                    ["email"] = new OpenApiString(new Faker("en").Person.Email),
                    ["role"] = new OpenApiString(new Faker("en").PickRandom(new[] { "User", "Admin" }))
                }
            };
        }
    }
}