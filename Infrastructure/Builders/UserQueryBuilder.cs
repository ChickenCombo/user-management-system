using Core.Entities;

namespace Infrastructure.Builders
{
    public class UserQueryBuilder(IQueryable<User> query)
    {
        private IQueryable<User> _query = query;

        public UserQueryBuilder FilterByFirstName(string? firstName)
        {
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                _query = _query.Where(u => u.FirstName.Contains(firstName));
            }

            return this;
        }

        public UserQueryBuilder FilterByMiddleName(string? middleName)
        {
            if (!string.IsNullOrWhiteSpace(middleName))
            {
                _query = _query.Where(u => u.MiddleName != null && u.MiddleName.Contains(middleName));
            }

            return this;
        }

        public UserQueryBuilder FilterByLastName(string? lastName)
        {
            if (!string.IsNullOrWhiteSpace(lastName))
            {
                _query = _query.Where(u => u.LastName.Contains(lastName));
            }

            return this;
        }

        public UserQueryBuilder FilterByRole(string? role)
        {
            if (!string.IsNullOrWhiteSpace(role))
            {
                _query = _query.Where(u => u.Role == role);
            }

            return this;
        }

        public UserQueryBuilder FilterByEmail(string? email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                _query = _query.Where(u => u.Email.Contains(email));
            }

            return this;
        }

        public IQueryable<User> Query()
        {
            return _query;
        }
    }
}