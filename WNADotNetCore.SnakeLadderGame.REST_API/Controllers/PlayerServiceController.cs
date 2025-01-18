using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WNADotNetCore.SnakeLadderGame.Database.Models;
using WNADotNetCore.SnakeLadderGame.Domain.Constants;
using WNADotNetCore.SnakeLadderGame.Domain.Features;
using WNADotNetCore.SnakeLadderGame.Domain.Model;

namespace WNADotNetCore.SnakeLadderGame.REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerServiceController : BaseController
    {
        private readonly PlayerService _playerService;

        public PlayerServiceController(PlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentPlayer()
        {
            var gameCode = GameSession.CurrentGameCode ?? "GC-01JHTVX";

            var currentPlayer = await _playerService.GetActivePlayer(gameCode);
            return Execute(currentPlayer);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterPlayer([FromBody] PlayerRegisterReqModel playerRegisterReqModel)
        {
            var newPlayer = new Player()
            {
                PlayerName = playerRegisterReqModel.PlayerName,
                GameCode = GameSession.CurrentGameCode ?? "GC-01JHTVX"

            };
            var result = await _playerService.RegisterPlayer(newPlayer);
            return Execute(result);
        }
    }
}
