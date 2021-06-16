

namespace Application.Constants
{
    public class Authorization
    {
        public enum Roles
        {
            Admin, Manager, User
        }
        public const Roles default_role = Roles.User;
    }
}
