using System;
using System.Collections.Generic;

namespace WNADotNetCore.SnakeLadderGame.Database.Models;

public partial class Game
{
    public int GameId { get; set; }

    public string GameCode { get; set; } = null!;

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
