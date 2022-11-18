using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessSystem
{
    public interface IPiece
    {
        PieceType Type { get;  }

        Player Player { get; }
    }
}
