using System;
using System.Collections.Generic;

namespace WNADotNetCore.SnakeLadderGame.Database.Models;

public partial class Player
{
    public int PlayerId { get; set; }

    public string PlayerName { get; set; } = null!;

    public int BoardPosition { get; set; }

    public string GameCode { get; set; } = null!;

    public virtual Game GameCodeNavigation { get; set; } = null!;

    public virtual ICollection<GamePlay> GamePlays { get; set; } = new List<GamePlay>();
}
