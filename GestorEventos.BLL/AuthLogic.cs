using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GestorEventos.BLL.Interfaces;
using GestorEventos.Models.DTO;
using GestorEventos.Models.Entities;
using GestorEventos.Models.Requests.Account;
using GestorEventos.Models.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace GestorEventos.BLL
{
    public class AuthLogic : IAuthLogic
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ISendGridLogic _sendGridLogic;
        private readonly IConfiguration _configuration;

        public AuthLogic(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ISendGridLogic sendGridLogic, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _sendGridLogic = sendGridLogic;
            _configuration = configuration;
        }

        public async Task<IdentityResult> RegisterAccount(RegisterAccountRequest register)
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

        public async Task EditAccount(EditAccountRequest edit)
        {
            var user = await _userManager.FindByIdAsync(edit.UserId);

            user.FirstName = edit.FirstName;
            user.LastName = edit.LastName;
            user.Email = edit.Email;
            user.Phone = edit.Phone;
            user.CellPhone = edit.CellPhone;
            user.Job = edit.Job;
            user.Organization = edit.Organization;
            user.Address1 = edit.Address1;
            user.Address2 = edit.Address2;
            user.City = edit.City;
            user.Country = edit.Country;

            await _userManager.UpdateAsync(user);
        }

        public async Task DeleteAccount(DeleteAccountRequest delete)
        {
            var user = await _userManager.FindByIdAsync(delete.UserId);
            await _userManager.DeleteAsync(user);
        }

        public async Task<ResetPasswordResult> ForgotPassword(ForgotPasswordRequest forgotPassword)
        {
            var user = await _userManager.FindByEmailAsync(forgotPassword.Email);

            if (user == null)
            {
                return new ResetPasswordResult(false);
            }

            try
            {
                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var actionUrl = _configuration.GetValue<string>("SiteOptions:ResetPassword");
                //var url = GenerateTokenUrl(actionUrl, user.Id, resetToken);
                var url = string.Format("{0}/{1}/{2}", actionUrl, user.Id, resetToken);
                var result = await _sendGridLogic.SendPasswordReset($"{user.FirstName} {user.LastName}", user.Email, url);

                return new ResetPasswordResult(true);
            }
            catch (Exception e)
            {
                return new ResetPasswordResult(false, ResetPasswordError.Exception, e);
            }
        }

        public async Task<IdentityResult> ResetPassword(ResetPasswordRequest resetPassword)
        {
            var user = await _userManager.FindByIdAsync(resetPassword.Id);
            return await _userManager.ResetPasswordAsync(user, resetPassword.Code, resetPassword.Password);
        }

        public async Task<IdentityResult> ChangePassword(ChangePasswordRequest changePassword)
        {
            var user = await _userManager.FindByIdAsync(changePassword.Id);
            return await _userManager.ChangePasswordAsync(user, changePassword.CurrentPassword, changePassword.NewPassword);
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

        public async Task<bool> ExistsUser(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }
    }
}