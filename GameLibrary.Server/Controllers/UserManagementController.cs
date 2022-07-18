using GameLibrary.Server.Data.Repositories;
using GameLibrary.Server.Models;
using GameLibrary.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt;
using System.Text.Json;

namespace GameLibrary.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserManagementController : ControllerBase
    {
        private readonly IDataGameLibrary _dataGameLibrary;

        // temporary hash key
        private string key { get; set; } = "EAtg@%454£b94g.gFGHh&s4*62)";

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
                string password = newUser.Password;
                newUser.Password = EncryptProvider.HMACSHA512(password, key);

                User response = await _dataGameLibrary.CreateUser(newUser);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            try
            {
                User response = await _dataGameLibrary.Login(user);
                if (response == null)
                {
                    return BadRequest();
                }

                var password = EncryptProvider.HMACSHA512(user.Password, key);

                if (password != response.Password)
                {
                    return BadRequest();
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("SendPwdRecoveryEmail")]
        public async Task<IActionResult> SendPwdRecoveryEmail(string email)
        {
            try
            {
                bool response = await _dataGameLibrary.CheckEmailForPwdRecovery(email);
                if (response)
                {
                    // email service to send email
                }
                else
                {
                    return Ok(false);
                }

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
