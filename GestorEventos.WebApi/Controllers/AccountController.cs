using GestorEventos.BLL.Interfaces;
using GestorEventos.Models.DTO;
using GestorEventos.Models.Requests.Account;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GestorEventos.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthLogic _authLogic;

        public AccountController(IAuthLogic authLogic)
        {
            _authLogic = authLogic;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterAccountRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid User Request");
            }

            if (string.IsNullOrWhiteSpace(request.Email))
            {
                return BadRequest("Email is Missing/Blank");
            }

            var exists = await _authLogic.ExistsUser(request.Email);

            if (exists)
            {
                return BadRequest("Email in use");
            }

            var result = await _authLogic.RegisterAccount(request);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("edit")]
        public async Task<IActionResult> Edit(EditAccountRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid User Request");
            }

            await _authLogic.EditAccount(request);

            return Ok();
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(DeleteAccountRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid User Request");
            }

            await _authLogic.DeleteAccount(request);

            return Ok();
        }

        [HttpPost]
        [Route("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid User Request");
            }

            var result = await _authLogic.ForgotPassword(request);

            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("reset-passsword")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            var result = await _authLogic.ResetPassword(request);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}