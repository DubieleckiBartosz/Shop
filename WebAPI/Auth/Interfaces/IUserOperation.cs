using Application.IdentityModels;
using Application.Wrapper;
using System.Threading.Tasks;
using WebAPI.RequestModels;

namespace WebAPI.Auth.Interfaces
{
    public interface IUserOperation
    {
        Task<ServiceResponse> RegisterAsync(RegisterModel registerModel);
        Task<ServiceResponse<AuthenticationModel>> LoginAsync(TokenRequest tokenRequest);
        Task<ServiceResponse> AddRoleAsync(AddRoleModel addRoleModel);
        Task<ServiceResponse> ResetPasswordAsync(ResetPasswordModel model);
        Task<ServiceResponse> DeleteRoleAsync(DeleteRoleModel deleteRole);
    }
}
