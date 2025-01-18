using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WNADotNetCore.SnakeLadderGame.Database.Models;
using WNADotNetCore.SnakeLadderGame.Domain.Constants;
using WNADotNetCore.SnakeLadderGame.Domain.Model;

namespace WNADotNetCore.SnakeLadderGame.Domain.Features
{
    public class GameEntryService
    {
        private readonly AppDbContext _db;

        public GameEntryService(AppDbContext db)
        {
            _db = db;
        }

        public static string GenerateGameCode()
        {
            var gameCode = "GC-" + Ulid.NewUlid().ToString();
            gameCode = gameCode.Length > 10 ? gameCode[..10] : gameCode;
            return gameCode;
        }

        public async Task<Result<string>> SaveGameInfo(Game gameInfo)
        {
            try
            {
                var newGame = new Game()
                {
                    GameCode = gameInfo.GameCode,
                };
                await _db.Games.AddAsync(newGame);
                await _db.SaveChangesAsync();
                GameSession.CurrentGameCode = newGame.GameCode;
                return Result<string>.Success($"Game Info Saved Successfully. GameCode: {newGame.GameCode}");
            }
            catch (Exception)
            {
                return Result<string>.Failure("Unsuccessful Saving Game Info");
            }
        }


    }
}
