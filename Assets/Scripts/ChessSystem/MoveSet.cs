using BoardSystem;
using CommandSystem;
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
        private readonly CommandQueue _commandQueue;

        protected Board<TPiece> Board => _board ;

        public MoveSet(Board<TPiece> board, CommandQueue commandQueue)
        {
            _board = board;
            _commandQueue = commandQueue;
        }


        public abstract List<Position> Positions(Position fromPosition);

        public virtual bool Execute(Position fromPosition, Position toPosition)
        {
            var command = new MoveCommand<TPiece>(_board, fromPosition, toPosition);


            
            _commandQueue.Execute(command);


            return true;


            //_board.Take(toPosition);
           //
           //return _board.Move(fromPosition, toPosition);
        }

    }

    public class MoveCommand<TPiece> : ICommand
    {
        Board<TPiece> _board;
        Position _fromPosition;
        Position _toPosition;
        TPiece _pieceTaken;

        public MoveCommand(Board<TPiece> board, Position fromPosition, Position toPosition)
        {
            _board = board;
            _fromPosition = fromPosition;
            _toPosition = toPosition;
        }

        public void Execute()
        {

            if(_board.TryGetPieceAt(_toPosition, out _pieceTaken))
                _board.Take(_toPosition);

            _board.Move(_fromPosition, _toPosition);
        }

        public void Undo()
        {
            _board.Move(_toPosition, _fromPosition);

            if (_pieceTaken != null)
                _board.Place(_toPosition, _pieceTaken);
        }
    }
}
