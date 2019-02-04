using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Tarea_1
{
    delegate void Backend (object data);
    public partial class Form1 : Form
    {
        
        private Backend log = data  => Console.WriteLine(data);
        private Backend error = data => Console.WriteLine("Error: {0}", data);


        public Form1()
        {
            InitializeComponent();
        }

        private void OnClickSend(object sender, EventArgs args)
        {
            log(cboSex.Text.ToString());
            bool ok = true;

            foreach (Control control in Controls)
            {
                string name = control.GetType().Name;
                
                
                switch(name)
                {
                    case "ComboBox":{
                        ok = ComboBoxHandler((ComboBox)control);
                    }
                    break;

                    default:
                    {

                    }
                    break;
                }
                
                if (!ok) OnError(control);
            }



            // this.Close();
        }

        private void OnClickClose(object sender, EventArgs args) => this.Close();

        private void OnClickReset(object sender, EventArgs args)
        {

        }

        private void OnError(Control control) => control.ForeColor = Color.Red;

        private bool ComboBoxHandler(ComboBox cboItem)
        {
            log(cboItem.Text);

            return true;
        }
    }
}
