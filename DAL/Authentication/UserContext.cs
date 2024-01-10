using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Authentication
{
    public class UserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUserId()
        {
            if (_httpContextAccessor?.HttpContext == null)
            {
                return string.Empty;
            }
            else
            {
                return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value ?? string.Empty;
            }
        }

        public string GetUsername()
        {
            if (_httpContextAccessor?.HttpContext == null)
            {
                return string.Empty;
            }
            else
            {
                return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Username")?.Value ?? string.Empty;
            }
        }


        public string GetToken()
        {
            string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            token = token.Substring(7, token.Length - 7);
            return token;
        }

        public string GetCurrentRole()
        {
            if (_httpContextAccessor?.HttpContext == null)
            {
                return string.Empty;
            }
            else
            {
                return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserType")?.Value ?? string.Empty;
            }
        }
        public string GetUserActions()
        {
            if (_httpContextAccessor?.HttpContext == null)
            {
                return string.Empty;
            }
            else
            {
                return _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "actions")?.Value ?? string.Empty;
            }
        }

        public string? GetUserMetadata()
        {
            return _httpContextAccessor?.HttpContext == null ? string.Empty : _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "metadata")?.Value;
        }
    }

}
