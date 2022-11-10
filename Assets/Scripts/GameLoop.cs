using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Assertions;

public class GameLoop : MonoBehaviour
{

    private Board _board;
    private void Start()
    {
        _board = new Board(PositionHelper.Rows, PositionHelper.Columns);
        _board.PieceMoved += (s, e) 
            => e.Piece.MoveTo(PositionHelper.WorldPosition(e.ToPosition));
        
        _board.PieceTaken += (s, e) 
            => e.Piece.Taken();

        _board.PiecePlaced += (s, e)
            => e.Piece.Placed(PositionHelper.WorldPosition(e.ToPosition));




        var pieceViews = FindObjectsOfType<PieceView>();
        foreach (var pieceView in pieceViews)
            _board.Place(PositionHelper.GridPosition(pieceView.WorldPosition), pieceView);

        var boardView = FindObjectOfType<BoardView>();
        boardView.PositionClicked += OnPositionClicked;


    }

    private void OnPositionClicked(object sender, PositionEventArgs e)
    {
        if(_board.TryGetPieceAt(e.Position, out var piece))
        {
            var toPosition = new Position(e.Position.X, e.Position.Y + 1);

            _board.Move(e.Position, toPosition);
        }
    }

}

