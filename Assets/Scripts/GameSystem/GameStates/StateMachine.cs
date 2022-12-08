﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSystem.GameStates
{
    public class StateMachine
    {
        private Dictionary<string, State> _states = new Dictionary<string, State>();

        private Stack<string> _currentStateNames = new Stack<string>();

        public string InitialState
        {
            set
            {
                _currentStateNames.Push(value);
                CurrentState.OnEnter();
            }
        }

        public State CurrentState => _states[_currentStateNames.Peek()];

        public void Register(string stateName, State state)
        {
            state.StateMachine = this;
            _states.Add(stateName, state);
        }

        public void MoveTo(string stateName)
        {
            CurrentState.OnSuspend();
            CurrentState.OnExit();


            _currentStateNames.Pop();
            _currentStateNames.Push(stateName);

            CurrentState.OnEnter();
            CurrentState.OnResume();
        }


        public void Push(string stateName)
        {
            CurrentState.OnSuspend();

            _currentStateNames.Push(stateName);

            CurrentState.OnEnter();
            CurrentState.OnResume();
        }

        public void Pop()
        {
            CurrentState.OnSuspend();
            CurrentState.OnExit();

            _currentStateNames.Pop();

            CurrentState.OnResume();
        }
    }
}
