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
    public class MenuState : State
    {
        private MenuView _menuView;

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            var op = SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Additive);
            op.completed += InitializeScene;

        }

        private void InitializeScene(AsyncOperation obj)
        {
            _menuView = GameObject.FindObjectOfType<MenuView>();
            if(_menuView != null)
                _menuView.PlayClicked +=  OnPlayClicked;
        }


        public override void OnExit()
        {
            base.OnExit();

            if (_menuView != null)
                _menuView.PlayClicked -= OnPlayClicked;

            SceneManager.UnloadSceneAsync("Menu");
        }

        private void OnPlayClicked(object sender, EventArgs e)
        {
            StateMachine.MoveTo(States.Playing);
        }
    }
}
