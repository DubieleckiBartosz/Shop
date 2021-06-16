using Application.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class UserContextService: IUserContextService
    {
        private readonly IHttpContextAccessor _httpContext;
        public UserContextService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public ClaimsPrincipal User => _httpContext.HttpContext?.User;
        public string GetUser =>User is null?null : _httpContext.HttpContext.User.FindFirst(c => c.Type == ClaimTypes.Role).Value;
    }
}
