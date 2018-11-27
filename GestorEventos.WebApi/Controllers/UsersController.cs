using GestorEventos.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GestorEventos.WebApi.Controllers
{
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
        [Route("")]
        public async Task<IActionResult> GetUsersToEnable()
        {
            var result = await _usersLogic.GetUsersToEnable();

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUser(string userId)
        {
            var result = await _usersLogic.GetUser(userId);

            return Ok(result);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> EnableUser(string userId, string roleId)
        {
            var result = await _usersLogic.EnableUser(userId, roleId, string.Empty);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> RejectUser(string userId)
        {
            var result = await _usersLogic.RejectUser(userId);

            if (result)
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