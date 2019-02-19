using GestorEventos.BLL.Interfaces;
using GestorEventos.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GestorEventos.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersLogic _usersLogic;

        public UsersController(IUsersLogic usersLogic)
        {
            _usersLogic = usersLogic;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetUsers()
        {    
            var result = await _usersLogic.GetAllUsers();

            return Ok(result);
        }

        [HttpGet]
        [Route("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var result = await _usersLogic.GetRoles();

            return Ok(result);
        }

        [HttpPost]
        [Route("assign-role")]
        public async Task<IActionResult> AssignRole(UserRoleDTO userRole)
        {
            await _usersLogic.AssignRole(userRole.UserId, userRole.Role);

            return Ok();
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetUser(string userId)
        {
            var result = await _usersLogic.GetUser(userId);

            return Ok(result);
        }


    }
}