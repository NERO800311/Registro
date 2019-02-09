using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using Tarea_1.Controls;

namespace Tarea_1
{
    public class ControlHandler
    {
        private Dictionary<string, Control> controlsCopy;
        private AbstractHandler handler;
        public static ControlHandler instance = new ControlHandler();
        private ControlHandler()
        {
            controlsCopy = new Dictionary<string, Control>();
        }

        public void Add(Control ctrl) => controlsCopy.Add(ctrl.Name, ctrl);
        private void OnError(Control control) => control.BackColor = Color.Red;

        public bool Eval(Control control)
        {
            bool ok = true;
            string name = control.GetType().Name;

            switch (name)
            {

                case "TextBox":
                    handler = new TextBoxHandler((TextBox)control);
                    break;

                case "DateTimePicker":
                    handler = new DateTimePickerHandler((DateTimePicker)control);
                    break;

                case "ComboBox":
                    handler = new ComboBoxHandler((ComboBox)control);
                    break;


                case "MaskedTextBox":
                    handler = new MaskedTextBoxHandler((MaskedTextBox)control);
                    break;

                default:
                    {
                        Utils.error($"control {control.Name} instance of {name} no has been configurated");
                    }
                    break;
            }

            if (!(ok = handler.GetData()))
            {
                Utils.error($"from control {control.Name}");
                OnError(control);
                // MessageBox.Show($"{control.Name}");
            }

            return ok;
        }


        public Control this[string index]
        {
            get { return controlsCopy[index]; }
        }
    }
}