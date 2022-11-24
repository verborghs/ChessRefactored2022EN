using BoardSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChessSystem
{
    public class Engine<TPiece>
        where TPiece : IPiece
    {
        private readonly Board<TPiece> _board;
        private readonly MoveSetCollection<TPiece> _moveSetCollection;

        public Engine(Board<TPiece> board)
        {
            _board = board;
            _moveSetCollection = new MoveSetCollection<TPiece>(_board);
        }

        public MoveSetCollection<TPiece> MoveSets
            => _moveSetCollection;

        public bool Move(Position fromPosition, Position toPosition)
        {
            if (!_board.IsValid(fromPosition))
                return false;

            if (!_board.IsValid(toPosition))
                return false;

            if (!_board.TryGetPieceAt(fromPosition, out var piece))
                return false;

            if (!MoveSets.TryGetMoveSet(piece.Type, out var moveSet))
                return false;

            if (!moveSet.Positions(fromPosition).Contains(toPosition))
                return false;

            return moveSet.Execute(fromPosition, toPosition);
        }
    }

}
