using System.Collections.Generic;
using System.Windows.Forms;

namespace Tarea_1
{
    public class ControlHandler
    {
        private Dictionary<string , Control> controlsCopy;
        public ControlHandler(){ controlsCopy = new Dictionary<string, Control>();}

        public void Add(Control ctrl)=>controlsCopy.Add(ctrl.Name, ctrl);


        public Control this[string index]
        {
            get { return controlsCopy[index]; }
        }
    }
}