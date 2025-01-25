
using API.DTOs;
using Bogus;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace API.Transformers
{
    internal sealed class DummyDataTransformer : IOpenApiSchemaTransformer
    {
        private static readonly Dictionary<Type, IOpenApiAny> _examples = [];

        public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken cancellationToken)
        {
            var type = context.JsonTypeInfo.Type;

            if (_examples.TryGetValue(type, out var example))
            {
                schema.Example = example;
            }

            return Task.CompletedTask;
        }

        public DummyDataTransformer()
        {
            _examples[typeof(CreateUserDto)] = new OpenApiObject
            {
                ["firstName"] = new OpenApiString(new Faker("en").Person.FirstName),
                ["middleName"] = new OpenApiString(new Faker("en").Person.LastName),
                ["lastName"] = new OpenApiString(new Faker("en").Person.LastName),
                ["role"] = new OpenApiString(new Faker("en").PickRandom(new[] { "User", "Admin" }))
            };

            _examples[typeof(UpdateUserDto)] = new OpenApiObject
            {
                ["firstName"] = new OpenApiString(new Faker("en").Person.FirstName),
                ["middleName"] = new OpenApiString(new Faker("en").Person.LastName),
                ["lastName"] = new OpenApiString(new Faker("en").Person.LastName),
                ["role"] = new OpenApiString(new Faker("en").PickRandom(new[] { "User", "Admin" }))
            };
        }
    }
}