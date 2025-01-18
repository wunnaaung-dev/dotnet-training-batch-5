using System;
using System.Collections.Generic;

namespace WNADotNetCore.SnakeLadderGame.Database.Models;

public partial class Board
{
    public int CellNo { get; set; }

    public string? Type { get; set; }

    public int? Destination { get; set; }
}
