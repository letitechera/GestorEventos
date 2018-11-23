using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GestorEventos.BLL.Interfaces;
using GestorEventos.Models.DTO;
using GestorEventos.Models.Entities;
using GestorEventos.Models.Responses;
using Microsoft.AspNetCore.Identity;

namespace GestorEventos.BLL
{
    public class AuthLogic : IAuthLogic
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ISendGridLogic _sendGridLogic;

        public AuthLogic(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ISendGridLogic sendGridLogic)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _sendGridLogic = sendGridLogic;
        }

        public async Task<IdentityResult> RegisterUser(RegisterDTO register)
        {
            var user = new AppUser
            {
                FirstName = register.FirstName,
                LastName = register.LastName,
                UserName = register.Email,
                Email = register.Email,
            };

            var result = await _userManager.CreateAsync(user, register.Password);

            // Notify admins that a new user has been registered in order to enable and assign a role.
            if (result.Succeeded)
            {
                var admins = await _userManager.GetUsersInRoleAsync(Core.Constants.RoleNameAdmin);

                if (admins != null)
                {
                    foreach (var admin in admins)
                    {
                        var adminUserId = admin.Id;
                        var adminUser = await _userManager.FindByIdAsync(adminUserId);

                        // TODO: Replace for redirectionLink
                        string redirectionLink = string.Empty;

                        //await SendRegistrationAlertMail(user, adminUser.Email, redirectionLink);
                    }
                }
            }

            return result;
        }

        public async Task EditUser(UserDTO userDTO)
        {
            var user = await _userManager.FindByIdAsync(userDTO.Id);

            user.FirstName = user.FirstName;
            user.LastName = user.LastName;

            await _userManager.UpdateAsync(user);
        }

        public async Task DeleteUser(UserDTO userDTO)
        {
            var user = await _userManager.FindByIdAsync(userDTO.Id);
            await _userManager.DeleteAsync(user);
        }

        public async Task<ResetPasswordResult> ForgotPassword(ForgotPasswordDTO forgotPassword, string actionUrl)
        {
            var user = await _userManager.FindByEmailAsync(forgotPassword.Email);

            if (user == null)
            {
                // TODO: Fix
                return null;
            }

            try
            {
                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                var url = GenerateTokenUrl(actionUrl, Core.Constants.SendGridAuthLink_ResetPassword, user.Id, resetToken);

                var result = await _sendGridLogic.SendPasswordReset($"{user.FirstName} {user.LastName}", user.Email, url.ToString());

                return new ResetPasswordResult(true);
            }
            catch (Exception e)
            {
                return new ResetPasswordResult(false, ResetPasswordError.Exception, e);
            }
        }

        public async Task<IdentityResult> ResetPassword(ResetPasswordDTO resetPassword)
        {
            var user = await _userManager.FindByIdAsync(resetPassword.Id);
            return await _userManager.ResetPasswordAsync(user, resetPassword.Code, resetPassword.Password);
        }

        private async Task<bool> SendRegistrationAlertMail(AppUser registered, string adminEmail, string redirectionLink)
        {
            if (registered == null)
            {
                return false;
            }

            await _sendGridLogic.SendUserRegistrationAlert(registered.Email, adminEmail, redirectionLink);

            return true;
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