using BoardSystem;
using ChessSystem;
using GameSystem.Helpers;
using GameSystem.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystem.GameStates
{
    public class PlayingState : State
    {
        private Board<PieceView> _board;
        private Engine<PieceView> _engine;

        private BoardView _boardView;

        public override void OnEnter()
        {
            base.OnEnter();

            _board = new Board<PieceView>(PositionHelper.Columns, PositionHelper.Rows);
            _board.PieceMoved += (s, e) => e.Piece.MoveTo(PositionHelper.WorldPosition(e.ToPosition));
            _board.PieceTaken += (s, e) => e.Piece.Taken();
            _board.PiecePlaced += (s, e) => e.Piece.Placed(PositionHelper.WorldPosition(e.ToPosition));

            _engine = new Engine<PieceView>(_board);

            var op = SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
            op.completed += InitializeScene;
        }

        private void InitializeScene(AsyncOperation obj)
        {
            _boardView = GameObject.FindObjectOfType<BoardView>();
            if(_boardView != null)
                _boardView.PositionClicked += OnPositionClicked;

            var pieceViews = GameObject.FindObjectsOfType<PieceView>();
            foreach (var pieceView in pieceViews)
                _board.Place(PositionHelper.GridPosition(pieceView.WorldPosition), pieceView);
        }

        public override void OnExit()
        {
            base.OnExit();

            if (_boardView != null)
                _boardView.PositionClicked -= OnPositionClicked;

            SceneManager.UnloadSceneAsync("Game");
        }

        private Position? _currentPosition;

        private void OnPositionClicked(object sender, PositionEventArgs e)
        {

            var pieceClicked = _board.TryGetPieceAt(e.Position, out var clickedPiece);
            var ownPieceClicked = pieceClicked && clickedPiece.Player == _engine.CurrentPlayer;

            if (ownPieceClicked)
            {
                _currentPosition = e.Position;
                var validPositions  = _engine.MoveSets.For(clickedPiece.Type).Positions(e.Position);
                _boardView.ActivePosition = validPositions;

            }
            else if (_currentPosition != null)
            {
                if (_engine.Move(_currentPosition.Value, e.Position))
                    _boardView.ActivePosition = new List<Position>(0);

            }                    
        }

    }
}
