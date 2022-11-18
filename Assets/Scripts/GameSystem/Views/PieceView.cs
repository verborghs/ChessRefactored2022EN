using ChessSystem;
using UnityEngine;

namespace GameSystem.Views
{
    public class PieceView : MonoBehaviour, IPiece
    {

        [SerializeField]
        private Player _player;

        [SerializeField]
        private PieceType _type;
        
        public Player Player => _player;

        public PieceType Type => _type;

        public Vector3 WorldPosition => transform.position;


        internal void MoveTo(Vector3 worldPosition)
        {
            transform.position = worldPosition;
        }

        internal void Taken()
        {
            gameObject.SetActive(false);
        }

        internal void Placed(Vector3 worldPosition)
        {
            transform.position = worldPosition;
            gameObject.SetActive(true);
        }
    }

}