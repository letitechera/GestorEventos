using GestorEventos.Models.DTO;
using GestorEventos.Models.Requests.Account;
using GestorEventos.Models.Responses;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace GestorEventos.BLL.Interfaces
{
    public interface IAuthLogic
    {
        Task<IdentityResult> RegisterAccount(RegisterAccountRequest register);
        Task EditAccount(EditAccountRequest user);
        Task DeleteAccount(DeleteAccountRequest user);     
        Task<ResetPasswordResult> ForgotPassword(ForgotPasswordRequest forgotPassword);
        Task<IdentityResult> ResetPassword(ResetPasswordRequest resetPassword);
        Task<IdentityResult> ChangePassword(ChangePasswordRequest changePassword);
        Task<bool> ExistsUser(string email);
    }
}