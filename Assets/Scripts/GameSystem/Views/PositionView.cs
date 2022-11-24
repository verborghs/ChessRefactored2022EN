using BoardSystem;
using GameSystem.Helpers;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GameSystem.Views
{

    [Serializable]
    public class ActivationChangedUnityEvent : UnityEvent<bool> { }

    public class PositionView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private UnityEvent OnActivate;

        [SerializeField]
        private UnityEvent OnDeactivate;

        [SerializeField]
        private ActivationChangedUnityEvent OnActivationChanged;


        private BoardView _parent;

        public Position GridPosition => PositionHelper.GridPosition(transform.position);

        private void Start()
            => _parent = GetComponentInParent<BoardView>();


        public void OnPointerClick(PointerEventData eventData)
            => _parent.ChildClicked(this);



        internal void Activate()
        {
            OnActivate?.Invoke();
            OnActivationChanged?.Invoke(true);
        }

        internal void Deactivate()
        {
            OnDeactivate?.Invoke();
            OnActivationChanged?.Invoke(false);
        }
    }
}