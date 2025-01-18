using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WNADotNetCore.SnakeLadderGame.Database.Models;
using WNADotNetCore.SnakeLadderGame.Domain.Constants;
using WNADotNetCore.SnakeLadderGame.Domain.Model;
using WNADotNetCore.SnakeLadderGame.Domain.Utils;

namespace WNADotNetCore.SnakeLadderGame.Domain.Features
{
    public class GamePlayService
    {
        private readonly AppDbContext _db;
        private readonly PlayerService _playerService;
        public GamePlayService(AppDbContext db, PlayerService playerService)
        {
            _db = db;
            _playerService = playerService;
        }

        public async Task<Result<DiceResModel>> RollDice()
        {
            try
            {
                var currentPlayer = await _playerService.GetActivePlayer(GameSession.CurrentGameCode ?? "GC-01JHWSP");
                if (currentPlayer?.Data == null)
                {
                    return Result<DiceResModel>.Failure("No active player found for the current game session.");
                }

                var diceRollNo = new Random().Next(1, 7);

                var currentPosition = await _db.Players
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(x => x.PlayerId == currentPlayer.Data.PlayerId);
                var initialPosition = currentPosition!.BoardPosition;
                var newPosition = CalculateNewPosition.CalculatePositionForDiceRoll(initialPosition, diceRollNo);
                currentPosition.BoardPosition = newPosition;
                _db.Entry(currentPosition).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                var result = new DiceResModel()
                {
                    PlayerId = currentPlayer.Data.PlayerId,
                    NewPosition = newPosition,
                };
                return Result<DiceResModel>.Success(result, message: $"Player: {currentPlayer.Data.PlayerName} move from {initialPosition} to {newPosition}");
            }
            catch (Exception ex)
            {
                return Result<DiceResModel>.Failure($"An error occurred while rolling the dice: {ex.Message}");
            }
        }


        public async Task<Result<string>> Move(MoveReqModel moveReq)
        {
            try
            {
                var currentPlayerId = moveReq.PlayerId;

                var cellType = moveReq.BoardInfo.Type;
                var destination = moveReq.BoardInfo.Destination;

                // Fetch the latest player position
                var playerPosition = await _db.Players
                                    .FirstOrDefaultAsync(x => x.PlayerId == currentPlayerId);
                var playerName = playerPosition!.PlayerName;
                var initialPosition = playerPosition!.BoardPosition;

                if (playerPosition!.BoardPosition >= 100)
                {
                    return Result<string>.Success($"🎉 Winner: {playerName} has reached the final position!");
                }

                string moveMessage;
                if (cellType == "Ladder")
                {
                    playerPosition.BoardPosition = (int)destination!;
                    moveMessage = $"🎉 climbed a ladder to position !";
                }
                else if (cellType == "Snake")
                {
                    playerPosition.BoardPosition = (int)destination!;
                    moveMessage = $"🐍 was bitten by a snake and slid down to position .";
                }
                else
                {
                    moveMessage = $"🎲 stayed at position.";
                }

                _db.Entry(playerPosition).State = EntityState.Modified;
                await _db.SaveChangesAsync();


                var playRecord = new GamePlay()
                {
                    PlayerId = currentPlayerId,
                    FromCell = initialPosition,
                    ToCell = destination ?? playerPosition.BoardPosition
                };
                await _db.GamePlays.AddAsync(playRecord);
                await _db.SaveChangesAsync();

                return Result<string>.Success($"Player {playerName} {moveMessage}");
            }
            catch (Exception ex)
            {
                return Result<string>.Failure($"An error occurred while processing the move: {ex.Message}");
            }
        }




    }
}
