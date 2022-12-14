using BoardSystem;
using CommandSystem;
using System;
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

        private Player _currentPlayer = Player.Player1;

        public Player CurrentPlayer => _currentPlayer;

        public Engine(Board<TPiece> board, CommandQueue commandQueue)
        {
            _board = board;
            _moveSetCollection = new MoveSetCollection<TPiece>(_board, commandQueue);
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

            if (piece.Player != CurrentPlayer)
                return false;

            if (!MoveSets.TryGetMoveSet(piece.Type, out var moveSet))
                return false;

            if (!moveSet.Positions(fromPosition).Contains(toPosition))
                return false;

            if (!moveSet.Execute(fromPosition, toPosition))
                return false;

            ChangePlayer();

             return true;
            
        }

        private void ChangePlayer()
        {
            _currentPlayer = (_currentPlayer == Player.Player1) ? Player.Player2 : Player.Player1;
        }
    }

}
