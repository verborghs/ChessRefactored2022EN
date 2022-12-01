using GameSystem.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystem.GameStates
{
    public class PlayingState : State
    {
        private BoardView _boardView;

        public override void OnEnter()
        {
            base.OnEnter();
            var op = SceneManager.LoadSceneAsync("Game", LoadSceneMode.Additive);
            op.completed += InitializeScene;
        }

        private void InitializeScene(AsyncOperation obj)
        {
            _boardView = GameObject.FindObjectOfType<BoardView>();
            if(_boardView != null)
                _boardView.PositionClicked += OnPositionClicked;
        }

        public override void OnExit()
        {
            base.OnExit();

            if (_boardView != null)
                _boardView.PositionClicked -= OnPositionClicked;

            SceneManager.UnloadSceneAsync("Game");
        }
        private void OnPositionClicked(object sender, PositionEventArgs e)
        {
            Debug.Log("Playing");
        }

    }
}
