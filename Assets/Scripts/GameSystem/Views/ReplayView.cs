﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace GameSystem.Views
{
    public class ReplayView : MonoBehaviour
    {
        public event EventHandler PreviousClicked;
        public event EventHandler NextClicked;

        [SerializeField]
        private Button _previous;

        [SerializeField]
        private Button _next;

        [SerializeField]
        private GameLoop _gameLoop;

        public void Previous()
            => OnPreviousClicked(EventArgs.Empty);


        public void Next()
            => OnNextClicked(EventArgs.Empty);


        public void Update()
        {
            if (_gameLoop._commandQueue.IsAtEnd)
                _next.interactable = false;
            else
                _next.interactable = true;

            if (_gameLoop._commandQueue.IsAtStart)
                _previous.interactable = false;
            else
                _next.interactable = true;
        }

        protected virtual void OnPreviousClicked(EventArgs eventArgs)
        {
            var handler = PreviousClicked;
            handler?.Invoke(this, eventArgs);
        }

        protected virtual void OnNextClicked(EventArgs eventArgs)
        {
            var handler = NextClicked;
            handler?.Invoke(this, eventArgs);
        }
    }
}
