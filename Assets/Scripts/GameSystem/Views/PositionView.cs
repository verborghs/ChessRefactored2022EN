using BoardSystem;
using GameSystem.Helpers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameSystem.Views
{
    public class PositionView : MonoBehaviour, IPointerClickHandler
    {
        private BoardView _parent;

        public Position GridPosition => PositionHelper.GridPosition(transform.position);

        private void Start()
            => _parent = GetComponentInParent<BoardView>();


        public void OnPointerClick(PointerEventData eventData)
            => _parent.ChildClicked(this);

    }
}