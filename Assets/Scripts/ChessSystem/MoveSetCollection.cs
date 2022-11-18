using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessSystem
{
    public class MoveSetCollection<TPiece>
        where TPiece : IPiece
    {

        private Dictionary<PieceType, MoveSet<TPiece>> _moveSets
            = new Dictionary<PieceType, MoveSet<TPiece>>();

        public IMoveSet For(PieceType type)
            => _moveSets[type];

        internal bool TryGetMoveSet(PieceType type, out MoveSet<TPiece> moveSet)
            => _moveSets.TryGetValue(type, out moveSet);
    }
}
