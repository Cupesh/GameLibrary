using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using GameLibrary.Server.Data.Repositories;
using GameLibrary.Server.Models;
using System.Text.Json;
using GameLibrary.Shared.Models;

namespace GameLibrary.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IDataGameLibrary _dataGameLibrary;

        public TestController(IDataGameLibrary dataGameLibrary)
        {
            _dataGameLibrary = dataGameLibrary;
        }

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            try
            {
                List<PSXGame> games = JsonSerializer.Deserialize<List<PSXGame>>(System.IO.File.ReadAllText(@"C:\games.json"));

                foreach(var game in games)
                {
                    try
                    {
                        await _dataGameLibrary.Populate(game);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                

                return Ok(games);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
