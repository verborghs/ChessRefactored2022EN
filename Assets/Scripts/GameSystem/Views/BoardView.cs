using BoardSystem;
using GameSystem.Helpers;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace GameSystem.Views
{
    public class PositionEventArgs : EventArgs
    {
        public Position Position { get; }

        public PositionEventArgs(Position position)
        {
            Position = position;
        }
    }

    public class BoardView : MonoBehaviour
    {
        public event EventHandler<PositionEventArgs> PositionClicked;
        private Dictionary<Position, PositionView> _positionViews = new Dictionary<Position, PositionView>();


        private List<Position> _activePosition = new List<Position>();

        public List<Position> ActivePosition {
            set {
                foreach (var position in _activePosition)
                    _positionViews[position].Deactivate();

                if (value == null)
                    _activePosition.Clear();
                else
                    _activePosition = value;

                foreach (var position in _activePosition)
                    _positionViews[position].Activate();
            }
        }
    
        private void OnEnable()
        {
            var positionViews = GetComponentsInChildren<PositionView>();
            foreach(var positionView in positionViews)
            {
                _positionViews.Add(positionView.GridPosition, positionView);
            }
        }

        internal void ChildClicked(PositionView positionView)
            => OnPositionClicked(new PositionEventArgs(positionView.GridPosition));

        protected virtual void OnPositionClicked(PositionEventArgs e)
        {
            var handler = PositionClicked;
            handler?.Invoke(this, e);
        }


    }

}
