using BoardSystem;
using CommandSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ChessSystem
{
    public class MoveSetCollection<TPiece>
        where TPiece : IPiece
    {

        private Dictionary<PieceType, MoveSet<TPiece>> _moveSets
            = new Dictionary<PieceType, MoveSet<TPiece>>();

        


        public MoveSetCollection(Board<TPiece> board, CommandQueue commandQueue)
        {
            _moveSets.Add(PieceType.Pawn,
                new ConfigurableMoveSet<TPiece>(
                    board,
                    commandQueue,
                    (b, p) => new MoveSetHelper<TPiece>(p, b)
                                    .Forward(1)
                                    .ValidPositions()
                ));

            _moveSets.Add(PieceType.Rook,
                new ConfigurableMoveSet<TPiece>(
                    board,
                    commandQueue,
                    (b, p) => new MoveSetHelper<TPiece>(p, b)
                                    .Forward()
                                    .Backward()
                                    .Left()
                                    .Right()
                                    .ValidPositions()
                ));

            _moveSets.Add(PieceType.Knight,
                new ConfigurableMoveSet<TPiece>(
                    board,
                    commandQueue,
                    (b, p) => new MoveSetHelper<TPiece>(p, b)
                                    .Collect(new Vector2Int(2, 1), 1)
                                    .Collect(new Vector2Int(2, -1), 1)
                                    .Collect(new Vector2Int(-2, 1), 1)
                                    .Collect(new Vector2Int(-2, -1), 1)
                                    .Collect(new Vector2Int(1, 2), 1)
                                    .Collect(new Vector2Int(1, -2), 1)
                                    .Collect(new Vector2Int(-1, 2), 1)
                                    .Collect(new Vector2Int(-1, -2), 1)
                                    .ValidPositions()
                )); ;

            _moveSets.Add(PieceType.Bishop,
                new ConfigurableMoveSet<TPiece>(
                    board,
                    commandQueue,
                    (b, p) => new MoveSetHelper<TPiece>(p, b)
                                .ForwardRight()
                                .ForwardLeft()
                                .BackwardRight()
                                .BackwardLeft()
                                .ValidPositions()
                ));

            _moveSets.Add(PieceType.Queen,
                new ConfigurableMoveSet<TPiece>(
                    board,
                    commandQueue,
                    (b, p) => new MoveSetHelper<TPiece>(p, b)
                        .Forward()
                        .ForwardRight()
                        .Right()
                        .BackwardRight()
                        .Backward()
                        .BackwardLeft()
                        .Left()
                        .ForwardLeft()
                        .ValidPositions()

                ));

            _moveSets.Add(PieceType.King, new ConfigurableMoveSet<TPiece>(
                    board,
                    commandQueue,
                    (b, p) => new MoveSetHelper<TPiece>(p, b)
                        .Forward(1)
                        .ForwardRight(1)
                        .Right(1)
                        .BackwardRight(1)
                        .Backward(1)
                        .BackwardLeft(1)
                        .Left(1)
                        .ForwardLeft(1)
                        .ValidPositions()
                ));

            _board = board;
        }


     


private readonly Board<TPiece> _board;

        public IMoveSet For(PieceType type)
            => _moveSets[type];

        internal bool TryGetMoveSet(PieceType type, out MoveSet<TPiece> moveSet)
            => _moveSets.TryGetValue(type, out moveSet);
    }
}
