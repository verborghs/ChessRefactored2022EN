using BoardSystem;
using ChessSystem;
using GameSystem.Helpers;
using GameSystem.Views;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem
{
    public class GameLoop : MonoBehaviour
    {

        private Board<PieceView> _board;
        private Engine<PieceView> _engine;

        private void Start()
        {
            _board = new Board<PieceView>(PositionHelper.Rows, PositionHelper.Columns);
            _board.PieceMoved += (s, e)
                => e.Piece.MoveTo(PositionHelper.WorldPosition(e.ToPosition));

            _board.PieceTaken += (s, e)
                => e.Piece.Taken();

            _board.PiecePlaced += (s, e)
                => e.Piece.Placed(PositionHelper.WorldPosition(e.ToPosition));

            _engine = new Engine<PieceView>(_board);


            var pieceViews = FindObjectsOfType<PieceView>();
            foreach (var pieceView in pieceViews)
                _board.Place(PositionHelper.GridPosition(pieceView.WorldPosition), pieceView);

            var boardView = FindObjectOfType<BoardView>();
            boardView.PositionClicked += OnPositionClicked;


        }

        private void OnPositionClicked(object sender, PositionEventArgs e)
        {

            var fromPosition = e.Position;

            if (_board.TryGetPieceAt(fromPosition, out var piece))
            {
                IMoveSet moveSet = _engine.MoveSets.For(piece.Type);
                List<Position> validPositions = moveSet.Positions(fromPosition);
                Position toPosition = validPositions[0];

                _engine.Move(fromPosition, toPosition);



            }
        }

    }
}