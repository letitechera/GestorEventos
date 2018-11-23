using GestorEventos.BLL.Interfaces;
using GestorEventos.Models.DTO;
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
        public async Task<IActionResult> Register(RegisterDTO request)
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

            var result = await _authLogic.RegisterUser(request);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        //[HttpPut]
        //[Route("edit")]
        //public async Task<IActionResult> Edit(RegisterDTO request)
        //{

        //}

        //[HttpDelete]
        //[Route("delete")]
        //public async Task<IActionResult> Delete(RegisterDTO request)
        //{

        //}
    }
}