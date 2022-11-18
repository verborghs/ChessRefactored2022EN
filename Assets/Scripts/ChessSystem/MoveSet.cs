using BoardSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessSystem
{
    internal abstract class MoveSet<TPiece> : IMoveSet
        where TPiece : IPiece
    {
        private readonly Board<TPiece> _board;

        public MoveSet(Board<TPiece> board)
        {
            _board = board;
        }


        public abstract List<Position> Positions(Position fromPosition);

        public virtual bool Execute(Position fromPosition, Position toPosition)
        {
            _board.Take(fromPosition);

            return _board.Move(fromPosition, toPosition);
        }
        
    }
}
