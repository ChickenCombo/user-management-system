using API.Handlers;

namespace API.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoint(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/user")
                .WithTags("User");

            group.MapGet("{id:guid}", UserHandler.GetById)
                .WithName("GetUserById")
                .WithSummary("Get User by ID")
                .WithDescription("Fetches the user details corresponding to the provided GUID.");

            group.MapGet("", UserHandler.GetAll)
                .WithName("GetAllUser")
                .WithSummary("Get All User")
                .WithDescription("Fetches a paginated list of users, with an optional query filter params.");

            group.MapPost("", UserHandler.Create)
                .WithName("CreateUser")
                .WithSummary("Create User")
                .WithDescription("Creates a new user based on the provided user data.");

            group.MapPut("{id:guid}", UserHandler.Update)
                .WithName("UpdateUser")
                .WithSummary("Update User")
                .WithDescription("Updates the user identified by the provided GUID and updated user data.");

            group.MapDelete("{id:guid}", UserHandler.Delete)
                .WithName("DeleteUser")
                .WithSummary("Delete User")
                .WithDescription("Deletes the user identified by the provided GUID.");
        }
    }
}