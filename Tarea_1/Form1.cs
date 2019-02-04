using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Tarea_1
{
    delegate void Backend (object data);
    public partial class Form1 : Form
    {
        
        private Backend log = data  => Console.WriteLine(data);
        private Backend error = data => Console.WriteLine("Error: {0}", data);

        private List<string> registryData;
        private StreamWriter registry;


        public Form1()
        {
            InitializeComponent();
            registryData = new List<string>();
            // registry = File.AppendText(@".reg");

            for (int index = 0; index < Controls.Count; index++)
            {
                if (Controls[index].GetType().Name != "Button")
                Controls[index].Click += new System.EventHandler(this.OnEdit);
            }
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

                if (!ok){
                    error($"from control {control.Name}");
                    OnError(control);
                }
            }



            // this.Close();
        }

        private void OnClickClose(object sender, EventArgs args) => this.Close();

        private void OnClickReset(object sender, EventArgs args)
        {
            Form1 resetedForm = new Form1();
            resetedForm.Show();
            this.Dispose(false);
        }

        private void OnError(Control control) => control.BackColor = Color.Red;
        private void OnEdit(object sender, EventArgs args) => ((Control)sender).BackColor = Color.White;

        private bool ComboBoxHandler(ComboBox cboItem)
        {
            log(cboItem.Name);
            bool text = false;

            foreach (string item in cboItem.Items)
                if( text = item.Equals(cboItem.Text))
                {
                    registryData.Add(cboItem.Text);
                    break;
                }
            

            return text;
        }
    }
}
