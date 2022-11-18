using BoardSystem;
using System.Collections.Generic;

namespace ChessSystem
{
    public interface IMoveSet
    {
        List<Position> Positions(Position fromPosition);
    }
}
