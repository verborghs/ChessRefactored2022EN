using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameSystem.Views
{

    public class MenuView : MonoBehaviour
    {
        public EventHandler PlayClicked;

        public void Play()
            => OnPlayClicked(EventArgs.Empty);

        protected virtual void OnPlayClicked(EventArgs eventArgs)
        {
            var handler = PlayClicked;
            handler?.Invoke(this, eventArgs);
        }

    }
}
