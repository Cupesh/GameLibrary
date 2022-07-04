using GameLibrary.Server.Data.Repositories;
using GameLibrary.Server.Models;
using GameLibrary.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GameLibrary.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserManagementController : ControllerBase
    {
        private readonly IDataGameLibrary _dataGameLibrary;

        public UserManagementController(IDataGameLibrary dataGameLibrary)
        {
            _dataGameLibrary = dataGameLibrary;
        }

        [HttpGet("CheckUserNameUniqueness")]
        public async Task<IActionResult> CheckUserNameUniqueness(string userName)
        {
            try
            {
                bool isUnique = await _dataGameLibrary.CheckUserNameUniqueness(userName);

                return Ok(isUnique);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] User newUser)
        {
            try
            {
                User response = await _dataGameLibrary.CreateUser(newUser);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
