using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WNADotNetCore.SnakeLadderGame.Domain.Model
{
    public enum EnumRespType
    {
        None,
        Pending,
        Success,
        ValidationError,
        SystemError,
        NotFound,
        DBError
    }
}
