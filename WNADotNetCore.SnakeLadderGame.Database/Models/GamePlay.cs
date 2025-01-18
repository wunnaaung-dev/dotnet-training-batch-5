using System;
using System.Collections.Generic;

namespace WNADotNetCore.SnakeLadderGame.Database.Models;

public partial class GamePlay
{
    public int MovementId { get; set; }

    public int PlayerId { get; set; }

    public int FromCell { get; set; }

    public int ToCell { get; set; }

    public virtual Player Player { get; set; } = null!;
}
