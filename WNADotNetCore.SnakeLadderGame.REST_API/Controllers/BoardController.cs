using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WNADotNetCore.SnakeLadderGame.Domain.Features;

namespace WNADotNetCore.SnakeLadderGame.REST_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardController : BaseController
    {
        private readonly GameBoardService _gameBoardService;

        public BoardController(GameBoardService gameBoardService)
        {
            _gameBoardService = gameBoardService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var board = await _gameBoardService.GetGameBoard();
            return Execute(board);
        }

        [HttpGet("{cellNo}")]
        public async Task<IActionResult> GetSingleCell(int cellNo)
        {
            var singleCell = await _gameBoardService.GetSingleCellInfo(cellNo);
            return Execute(singleCell);
        }
    }
}
