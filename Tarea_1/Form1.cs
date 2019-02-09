using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
using Tarea_1;

namespace Tarea_1
{
    public partial class Form1 : Form
    {
        
        private DataHandler registry;
        private ControlHandler controlHandler;


        public Form1()
        {
            InitializeComponent();
            registry = DataHandler.instance;
            controlHandler = ControlHandler.instance;

            for (int index = 0; index < Controls.Count; index++)
            {
                if (Controls[index].GetType().Name != "Button")
                {
                    Controls[index].Click += new System.EventHandler(this.OnEdit);
                    Controls[index].ForeColor = Color.Black;
                }
                controlHandler.Add(Controls[index]);
            }
        }

        private void OnClickSend(object sender, EventArgs args)
        {
           
        }

        private void OnClickClose(object sender, EventArgs args) => this.Close();

        private void OnClickReset(object sender, EventArgs args)
        {
            foreach (Control control in Controls){
            string type = control.GetType().Name;
            Utils.log(type);
                if(type != "Button" && type != "Label")
                {
                    Control tmpControl = controlHandler[control.Name];

                    control.BackColor = tmpControl.BackColor;
                    control.Text = tmpControl.Text;
                }
            }
        }

        
        private Events OnEdit = (sender, args) => ((Control)sender).BackColor = Color.White;

        private bool ComboBoxHandler(ComboBox cboItem)
        {
            bool text = false;

            foreach (string item in cboItem.Items)
                if( text = item.Equals(cboItem.Text))
                {
                    Utils.log(cboItem.Name + ": " +cboItem.Text);
                    registry.Add(cboItem.Name + ": " +cboItem.Text);
                    break;
                }
            

            return text;
        }

        private void MaskedTextBoxValidating(object sender, CancelEventArgs args)
        {
            Utils.log(((Control)sender).Name);
        }

        private bool DateTimePickerHandler(DateTimePicker dateTimePicker)
        {
            DateTime dateTime = dateTimePicker.Value.Date;
            TimeSpan timeSpan = DateTime.Now - dateTime;
            registry.Add($"{dateTimePicker.Name}: Value = {timeSpan.ToString()} ");
            Utils.log(timeSpan.ToString());
            return true;
        }

        public bool TextBoxHandler(TextBox textBox)
        {
            string text = textBox.Text;

            if (string.IsNullOrEmpty(text)) return false;

            registry.Add($"{textBox.Name}: {text}");
            return true;
        }

        public bool MaskedTextBoxHandler(MaskedTextBox maskedTextBox)
        {
            string text = maskedTextBox.Text;

            if (string.IsNullOrEmpty(text)) return false;

            DateTime date;
            if (!DateTime.TryParseExact(
                text,
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal,
                out date        
            )){
                Utils.error(text + " is invalid date");
                return false;
            }
            
            registry.Add($"{maskedTextBox.Name}: {text}");
            return true;
        }
    }
}
