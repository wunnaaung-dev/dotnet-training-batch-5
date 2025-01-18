using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WNADotNetCore.SnakeLadderGame.Database.Models;
using WNADotNetCore.SnakeLadderGame.Domain.Model;

namespace WNADotNetCore.SnakeLadderGame.Domain.Features
{
    public class GameBoardService
    {
        private readonly AppDbContext _db;

        public GameBoardService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Result<List<Board>>> GetGameBoard()
        {
            var gameBoard = await _db.Boards.AsNoTracking().ToListAsync();
            return Result<List<Board>>.Success(gameBoard);
        }

        public async Task<Result<Board>> GetSingleCellInfo(int cellNo)
        {
            var singleCell = await _db.Boards
                .AsNoTracking()
                .FirstOrDefaultAsync(board => board.CellNo == cellNo);

            if (singleCell is null)
            {
                return Result<Board>.Failure("Cell not found.");
            }

            return Result<Board>.Success(singleCell);
        }


    }
}
