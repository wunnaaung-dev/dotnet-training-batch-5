using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WNADotNetCore.SnakeLadderGame.Domain.Features;
using WNADotNetCore.SnakeLadderGame.Domain.Model;

namespace WNADotNetCore.SnakeLadderGame.REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamePlayServiceController : BaseController
    {
        private readonly GamePlayService _gamePlayService;
        private readonly GameBoardService _gameBoardService;


        public GamePlayServiceController(GamePlayService gamePlayService, GameBoardService gameBoardService)
        {
            _gamePlayService = gamePlayService;
            _gameBoardService = gameBoardService;
        }

        [HttpPost]
        public async Task<IActionResult> Play()
        {
            var playerPositionAfterDice = await _gamePlayService.RollDice();
            var gameBoardInfo = await _gameBoardService.GetSingleCellInfo(playerPositionAfterDice.Data.NewPosition);

            var newMove = new MoveReqModel()
            {
                PlayerId = playerPositionAfterDice.Data.PlayerId,
                BoardInfo = gameBoardInfo.Data
            };
            var result = await _gamePlayService.Move(newMove);
            return Execute(result);
        }
    }
}




