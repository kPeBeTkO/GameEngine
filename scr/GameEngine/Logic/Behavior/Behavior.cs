using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Logic.Behavior
{
    public class Behavior
    {
        private List<Control> controls = new List<Control>();
        public GameObject obj;
        public void AddControl(Control control)
        {
            controls.Add(control);
        }
        public void Work(EventArgs args)
        {
            /*lock (obj.Body.Location)
            {
                foreach (var control in controls)
                    if (control.Event == args)
                        control.EventHandler(obj);
            }*/
        }
    }
}
