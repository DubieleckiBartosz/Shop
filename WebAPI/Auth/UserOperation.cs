using Application.Constants;
using Application.Exceptions;
using Application.IdentityModels;
using Application.Wrapper;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Auth.Interfaces;
using WebAPI.RequestModels;
using WebAPI.Settings;

namespace WebAPI.Auth
{
    public class UserOperation:IUserOperation
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        public UserOperation(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager,IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwt = jwt.Value;
        }

        public async Task<ServiceResponse> RegisterAsync(RegisterModel registerModel)
        {
            ServiceResponse response = new ServiceResponse();
            var user = new ApplicationUser
            {
                Email = registerModel.Email,
                FirstName = registerModel.FirstName,
                LastName = registerModel.LastName,
                UserName = registerModel.UserName
            };
            var userExist =await _userManager.FindByEmailAsync(registerModel.Email);
            if(userExist is null)
            {
                var result = await _userManager.CreateAsync(user, registerModel.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Authorization.default_role.ToString());

                    response.Message= $"User registered with username {user.UserName}";
                }
                else
                {
                    response.Success = false;
                    response.Message= "Something went wrong. Check your data and try again. ";
                }
            }
            else
            {
                response.Success = false;
                response.Message= $"Email {user.Email} is already registered";
            }
            return response;
        }



        public async Task<ServiceResponse<AuthenticationModel>> LoginAsync(TokenRequest tokenRequest)
        {
            ServiceResponse<AuthenticationModel> response = new ServiceResponse<AuthenticationModel>();
            var authModel = new AuthenticationModel();
            var user = await _userManager.FindByEmailAsync(tokenRequest.Email);
           
            if (user is null) throw new NotFoundException($"You need to register");
            
            if(await _userManager.CheckPasswordAsync(user, tokenRequest.Password))
            {
                JwtSecurityToken jwtSecurityToken = await CreateToken(user);

                authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                authModel.Email = user.Email;
                authModel.UserName = user.UserName;
                var roleList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                authModel.Roles = roleList.ToList();
                response.Data = authModel;
                return response;
            }
            response.Message = $"Incorrect Credentials for user {user.Email}";
            response.Success = false;
            return response;
        }

        private async Task<JwtSecurityToken> CreateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            
            var roleClaims = new List<Claim>();
            foreach (var item in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, item));
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier,user.Id)
            }.Union(userClaims).Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var securityToken = new JwtSecurityToken(

                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);
            return securityToken;
                
                
        }

        public async Task<ServiceResponse> AddRoleAsync(AddRoleModel addRoleModel)
        {     
            ServiceResponse response = new ServiceResponse(); 
            var user = await _userManager.FindByEmailAsync(addRoleModel.Email);
            if (user is null) throw new NotFoundException($"Not found user with email: {addRoleModel.Email}");
           if(await _userManager.CheckPasswordAsync(user, addRoleModel.Password))
            {
                var roleExist = Enum.GetNames(typeof(Authorization.Roles)).Any(c => c.ToLower() == addRoleModel.Role.ToLower());
                if (roleExist)
                {
                    var role = Enum.GetValues(typeof(Authorization.Roles)).Cast<Authorization.Roles>().Where(c => c.ToString().ToLower() == addRoleModel.Role.ToLower()).FirstOrDefault();
                    await _userManager.AddToRoleAsync(user,role.ToString());
                    response.Message = $"Added {addRoleModel.Role} to user {addRoleModel.Email}";
                    return response;
                }
                response.Success = false;
                response.Message = $"{addRoleModel.Role} Not found";
;               return response;
            }
            response.Success = false;
            response.Message = $"Incorrect Credentials for user {user.Email}";
            return response;
        }

        public async Task<ServiceResponse> ResetPasswordAsync(ResetPasswordModel model)
        {
            var user =await _userManager.FindByEmailAsync(model.Email);         
            if (user is null) throw new NotFoundException($"Not found user with email {model.Email}");
            ServiceResponse response = new ServiceResponse();

            if(await _userManager.CheckPasswordAsync(user, model.CurrentPassword))
            {
                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, model.NewPassword);
                response.Message = "Password is changed for user";
                return response;
            }
            response.Success = false;
            response.Message = "Check your data and try again";
            return response;
        }

        public async Task<ServiceResponse> DeleteRoleAsync(DeleteRoleModel deleteRole)
        {
            ServiceResponse response = new ServiceResponse();
            var user =await _userManager.FindByEmailAsync(deleteRole.Email);
            if (user is null) throw new NotFoundException($"Not found user with email: {deleteRole.Email}");

            var roleExist = Enum.GetNames(typeof(Authorization.Roles)).Any(c => c.ToLower() == deleteRole.Role.ToLower());

            if (roleExist)
            {
                var result = await _userManager.RemoveFromRoleAsync(user, deleteRole.Role);
                if (result.Succeeded)
                {
                    response.Message = $"Role has been removed user {user.Email}";
                    return response;
                }
                response.Message = $"Remove failed";
                return response;
            }
            response.Success = false;
            response.Message = $"{deleteRole} role not found";
            return response;
        }
    }
}
