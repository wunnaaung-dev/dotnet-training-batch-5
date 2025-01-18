using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WNADotNetCore.SnakeLadderGame.Database.Models;
using WNADotNetCore.SnakeLadderGame.Domain.Features;
using WNADotNetCore.SnakeLadderGame.Domain.Model;

namespace WNADotNetCore.SnakeLadderGame.REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameEntryServiceController : BaseController
    {
        private readonly GameEntryService _gameEntryService;

        public GameEntryServiceController(GameEntryService gameEntryService)
        {
            _gameEntryService = gameEntryService;
        }

        [HttpPost]
        public async Task<IActionResult> StartNewGame()
        {
            var gameCode = GameEntryService.GenerateGameCode();

            var newGame = new Game()
            {
                GameCode = gameCode,
            };
            var result = await _gameEntryService.SaveGameInfo(newGame);
            return Execute(result);
        }
    }
}
