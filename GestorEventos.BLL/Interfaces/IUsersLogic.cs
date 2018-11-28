using GestorEventos.Models.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestorEventos.BLL.Interfaces
{
    public interface IUsersLogic
    {
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<IEnumerable<UserDTO>> GetUsersToEnable();
        Task<UserDTO> GetUser(string userId);
        Task<bool> EnableUser(string userId, string roleId, string actionUrl);
        Task<bool> RejectUser(string userId);
    }
}
