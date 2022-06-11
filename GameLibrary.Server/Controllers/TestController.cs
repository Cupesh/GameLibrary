using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using GameLibrary.Server.Data.Repositories;
using GameLibrary.Server.Models;

namespace GameLibrary.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        private readonly IDataGameLibrary _dataTrailblazer;

        public TestController(IDataGameLibrary dataTrailblazer)
        {
            _dataTrailblazer = dataTrailblazer;
        }

        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            try
            {
                //test
                var data = await _dataTrailblazer.Test();

                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
