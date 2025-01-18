using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WNADotNetCore.SnakeLadderGame.Domain.Utils
{
    public static class CalculateNewPosition
    {
        public static int CalculatePositionForDiceRoll(int currentPosition, int diceRoll)
        {
            int newPosition = currentPosition + diceRoll;
            
            if (newPosition >= 100)
            {
                newPosition = 100;
            }

            return newPosition;
        }
    }
}
