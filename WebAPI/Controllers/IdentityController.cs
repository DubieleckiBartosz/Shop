using Application.IdentityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading.Tasks;
using WebAPI.Auth.Interfaces;
using WebAPI.RequestModels;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IUserOperation _userOperation;
        public IdentityController(IUserOperation userOperation)
        {
            _userOperation = userOperation;
        }

        [SwaggerOperation(Summary ="Register user")]
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody]RegisterModel registerModel)
        {          
            return Ok(await _userOperation.RegisterAsync(registerModel));
        }
       
        [SwaggerOperation(Summary = "Login user")]
        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser([FromBody]TokenRequest tokenRequest)
        {
            var result = await _userOperation.LoginAsync(tokenRequest);
            return Ok(result);
        }
       
        [SwaggerOperation(Summary = "Add role to user")]
        [HttpPost("AddRole")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddRole([FromBody] AddRoleModel roleModel)
        {
            var result = await _userOperation.AddRoleAsync(roleModel);
            return Ok(result);
        }
       
        [SwaggerOperation(Summary = "Create new account password")]
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody]ResetPasswordModel reset)
        {
            return Ok(await _userOperation.ResetPasswordAsync(reset));
        }
      
        [SwaggerOperation(Summary = "Remove a role for  user")]
        [HttpDelete("DeleteRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRoleUser([FromBody]DeleteRoleModel deleteRole)
        {
            var result =await _userOperation.DeleteRoleAsync(deleteRole);
            return Ok(result);
        }
    }
}
