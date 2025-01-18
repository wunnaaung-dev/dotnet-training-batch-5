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
    public class PlayerService
    {
        private readonly AppDbContext _db;
        private static Dictionary<string, List<Player>> _gamePlayers = new();
        public PlayerService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Result<string>> RegisterPlayer(Player player)
        {
            try
            {
                var isGameCodeExist = await _db.Games
                                .AsNoTracking()
                                .FirstOrDefaultAsync(x => x.GameCode == player.GameCode);
                if (isGameCodeExist is null)
                {
                    return Result<string>.NotFound("Game Code Does Not Exist");
                }

                var newPlayer = new Player()
                {
                    PlayerName = player.PlayerName,
                    GameCode = player.GameCode,
                    BoardPosition = 0
                };
                await _db.AddAsync(newPlayer);
                await _db.SaveChangesAsync();

                return Result<string>.Success("Player Registered Successfully");
            }
            catch (Exception)
            {
                return Result<string>.Failure("Player Cannot Register");
            }

        }

        public async Task<Result<Player>> GetActivePlayer(string gameCode)
        {
            try
            {
                if (!_gamePlayers.ContainsKey(gameCode))
                {
                    var players = await _db.Players
                    .AsNoTracking()
                    .Where(x => x.GameCode == gameCode)
                    .OrderBy(x => Guid.NewGuid())
                    .ToListAsync();

                    if (players == null || !players.Any())
                    {
                        return Result<Player>.Failure("No players found for the given game code.");
                    }

                    _gamePlayers[gameCode] = players;
                }

                var playersList = _gamePlayers[gameCode];
                var nextPlayer = playersList.First();
                _gamePlayers[gameCode] = playersList.Skip(1).Concat(playersList.Take(1)).ToList();


                return Result<Player>.Success(nextPlayer);

            }
            catch (Exception)
            {
                return Result<Player>.Failure($"Cannot get the next player");
            }
        }
    }
}
