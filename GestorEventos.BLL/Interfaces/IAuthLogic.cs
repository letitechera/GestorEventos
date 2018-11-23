using GestorEventos.Models.DTO;
using GestorEventos.Models.Responses;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace GestorEventos.BLL.Interfaces
{
    public interface IAuthLogic
    {
        Task<IdentityResult> RegisterUser(RegisterDTO register);
        Task EditUser(UserDTO user);
        Task DeleteUser(UserDTO user);     
        Task<ResetPasswordResult> ForgotPassword(ForgotPasswordDTO forgotPassword, string actionUrl);
        Task<IdentityResult> ResetPassword(ResetPasswordDTO resetPassword);
    }
}