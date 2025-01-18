using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WNADotNetCore.SnakeLadderGame.Database.Models;

namespace WNADotNetCore.SnakeLadderGame.Domain.Model
{
    public class MoveReqModel
    {
        public int PlayerId { get; set; }

        public required Board BoardInfo { get; set; }
    }
}
