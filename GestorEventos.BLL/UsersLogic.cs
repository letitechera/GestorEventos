using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using GestorEventos.BLL.Interfaces;
using GestorEventos.Models.DTO;
using GestorEventos.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace GestorEventos.BLL
{
    public class UsersLogic : IUsersLogic
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ISendGridLogic _sendGridLogic;
        private readonly IHttpContextAccessor _http;

        public UsersLogic(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ISendGridLogic sendGridLogic, IHttpContextAccessor http)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _sendGridLogic = sendGridLogic;
            _http = http;
        }

        public async Task<bool> EnableUser(string userId, string roleId, string actionUrl)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var role = await _roleManager.FindByIdAsync(roleId);

            // If user or role are wrong return false
            if (user == null || role == null)
            {
                return false;
            }

            user.Enabled = true;

            IdentityResult result = await _userManager.UpdateAsync(user);

            // Update user roles
            var currentRoles = await _userManager.GetRolesAsync(user);

            if (currentRoles.Any())
            {
                await _userManager.RemoveFromRolesAsync(user, currentRoles.ToArray());
            }

            await _userManager.AddToRoleAsync(user, role.Name);

            await SendEmailValidationToken(user.Email, actionUrl);

            return result.Succeeded;
        }

        public async Task<bool> RejectUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            // If user is wrong return false
            if (user == null)
            {
                return false;
            }

            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            var current = _http.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return _userManager.Users.Where(u => u.Email != current).Select(u => new UserDTO(u));
        }

        public async Task<IEnumerable<IdentityRole>> GetRoles()
        {
            return _roleManager.Roles;
        }

        public async Task AssignRole(string userId, string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<UserDTO> GetUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return new UserDTO(user);
        }

        public async Task<IEnumerable<UserDTO>> GetUsersToEnable()
        {
            var adminRole = _roleManager.Roles.FirstOrDefault(r => r.Name == Core.Constants.RoleNameAdmin);

            if (adminRole == null)
            {
                return null;
            }

            var users = from user in _userManager.Users
                        //where user.Roles.All(r => r.RoleId != adminRole.Id) && user.Enabled == null
                        select user;

            return users.Select(u => new UserDTO(u));
        }

        private async Task<bool> SendEmailValidationToken(string email, string actionUrl)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return false;
            }

            var token = await GenerateEmailToken(user);

            Uri callbackUrl = GenerateTokenUrl(actionUrl, Core.Constants.SendGridAuthLink_ConfirmEmail, user.Id, token);

            var result = await _sendGridLogic.SendEmailValidation(string.Format("{0} {1}", user.FirstName, user.LastName), user.Email, callbackUrl.ToString());

            return true;
        }

        private async Task<string> GenerateEmailToken(AppUser user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        private Uri GenerateTokenUrl(string actionUrl, string oauthLink, params string[] tokens)
        {
            Uri callbackUrl = new Uri(actionUrl);

            var paramList = new List<string>();
            paramList.AddRange(tokens.Select(HttpUtility.UrlEncode));

            var confirmUrl = string.Format(oauthLink, paramList.ToArray());

            callbackUrl = new Uri(callbackUrl, confirmUrl);

            return callbackUrl;
        }
    }
}