using BoardSystem;
using ChessSystem;
using CommandSystem;
using GameSystem.GameStates;
using GameSystem.Helpers;
using GameSystem.Views;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem
{
    public class GameLoop : MonoBehaviour
    {

        private StateMachine _stateMachine;
        public CommandQueue _commandQueue;


        private void Start()
        {
            _commandQueue = new CommandQueue();

            _stateMachine = new StateMachine();
            _stateMachine.Register(States.Menu, new MenuState());
            _stateMachine.Register(States.Playing, new PlayingState(_commandQueue));
            _stateMachine.Register(States.Replaying, new ReplayState(_commandQueue));

            _stateMachine.InitialState = States.Menu;
        }

    }
}