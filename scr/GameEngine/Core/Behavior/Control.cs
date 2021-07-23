using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Core.Behavior
{
    abstract public class Control
    {
        public delegate void EventDelegate();
        public event EventDelegate Event;
        public Control(EventArgs args, Action eventHandler)
        {
            Event = args;
            Event += new EventDelegate(eventHandler);
        }
        public void InvokeEvent(GameObject obj)
        {
            Event.Invoke();
        }
    }
}
