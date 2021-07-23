using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Logic.Behavior
{
    abstract public class Control
    {
        public event Action Event;
        public Control(EventArgs args, Action eventHandler)
        {
            Event += eventHandler;
        }
        public void InvokeEvent(GameObject obj)
        {
            Event.Invoke();
        }
    }
}
