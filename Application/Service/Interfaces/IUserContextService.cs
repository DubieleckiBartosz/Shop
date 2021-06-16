using System.Security.Claims;


namespace Application.Service.Interfaces
{
    public interface IUserContextService
    {
        ClaimsPrincipal User { get; }
        string GetUser { get; }
    }
}
