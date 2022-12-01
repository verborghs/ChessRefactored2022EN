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

        private string _currentStateName;

        public string InitialState
        {
            set
            {
                _currentStateName = value;
                CurrentState.OnEnter();
            }
        }

        public State CurrentState => _states[_currentStateName];

        public void Register(string stateName, State state)
        {
            state.StateMachine = this;
            _states.Add(stateName, state);
        }

        public void MoveTo(string stateName)
        {
            CurrentState.OnExit();

            _currentStateName = stateName;

            CurrentState.OnEnter();
        }

    }
}
