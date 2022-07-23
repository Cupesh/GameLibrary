using GameLibrary.Server.Data.Repositories;
using GameLibrary.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameQueriesController : ControllerBase
    {
        private readonly IDataGameLibrary _dataGameLibrary;
        public GameQueriesController(IDataGameLibrary dataGameLibrary)
        {
            _dataGameLibrary = dataGameLibrary;
        }

        [HttpGet("FindGamesBySearchText")]
        public async Task<IActionResult> FindGamesBySearchText(string searchText)
        {
            try
            {
                List<PSXGame> games = await _dataGameLibrary.FindGamesBySearchText(searchText);
                return Ok(games);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
