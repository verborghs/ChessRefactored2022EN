using CommandSystem;
using GameSystem.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem.GameStates
{
    public class ReplayState : State
    {
        private readonly CommandQueue _commandQueue;
        private ReplayView _replayView;

        public ReplayState(CommandQueue commandQueue)
        {
            _commandQueue = commandQueue;
        }

        public override void OnEnter()
        {
            _commandQueue.Previous();
        }

        public override void OnResume()
        {
            _replayView = GameObject.FindObjectOfType<ReplayView>();
            if(_replayView != null)
            {
                _replayView.PreviousClicked += OnPreviousClicked;
                _replayView.NextClicked += OnNextClicked;
            }
        }

        public override void OnSuspend()
        {
            if (_replayView != null)
            {
                _replayView.PreviousClicked -= OnPreviousClicked;
                _replayView.NextClicked -= OnNextClicked;
            }
        }

        private void OnPreviousClicked(object sender, EventArgs e)
            =>  _commandQueue.Previous();
       

        private void OnNextClicked(object sender, EventArgs e)
        { 
             _commandQueue.Next();
            if (_commandQueue.IsAtEnd)
                StateMachine.Pop();
        }
            
        
    }
}
