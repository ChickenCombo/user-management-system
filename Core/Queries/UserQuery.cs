namespace Core.Queries
{
    public class UserQuery : PageQuery
    {
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
    }
}