using System.Collections.Generic;


namespace Application.IdentityModels
{
    public class AuthenticationModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
    }
}
