using System.Windows.Forms;

namespace Tarea_1.Controls
{
    public class AbstractHandler
    {
        protected DataHandler data;
        protected string name, value;
        protected Control control;
        public AbstractHandler(Control control)
        {
            data = DataHandler.instance;
            this.control = control;
        }

        public void Save() => data.ToStream(name, value);

        public virtual bool GetData()
        {
            name = control.Name;
            value = control.Text;

            return true;
        }
    }
}