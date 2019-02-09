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
using System.Globalization;


namespace Tarea_1
{
    public delegate void Backend (object data);
    public partial class Form1 : Form
    {
        
        
        private List<string> registryData;
        private StreamWriter registry;
        private ControlHandler controlHandler;


        public Form1()
        {
            InitializeComponent();
            registryData = new List<string>();
            // registry = File.AppendText(@".reg");
            controlHandler = new ControlHandler();

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
            bool ok = true;

            foreach (Control control in Controls)
            {
                string name = control.GetType().Name;

                if (name == "Label" || name == "Button") continue;
                Utils.log(name);

                switch(name)
                {
                    case "DateTimePicker": ok = DateTimePickerHandler((DateTimePicker)control);
                    break;

                    case "ComboBox": ok = ComboBoxHandler((ComboBox)control);
                    break;

                    case "TextBox": ok = TextBoxHandler((TextBox)control);
                    break;

                    case "MaskedTextBox": ok = MaskedTextBoxHandler((MaskedTextBox)control);
                    break;

                    default:
                    {
                        Utils.error($"control {control.Name} instance of {name} no has been configurated");
                    }
                    break;
                }

                if (!ok){
                    Utils.error($"from control {control.Name}");
                    OnError(control);
                    // MessageBox.Show($"{control.Name}");
                }
            }
        }

        private void OnClickClose(object sender, EventArgs args){
            foreach (string item in registryData)
            {
                Utils.log(item);
            }
            this.Close();
        } //=> this.Close();

        private void OnClickReset(object sender, EventArgs args)
        {
            foreach (Control control in Controls){
            string type = control.GetType().Name;
            Utils.log(type);
                if(type != "Button" && type != "Label")
                {
                    control.Text = controlHandler[control.Name];
                    control.BackColor = Color.White;
                }
            }
        }

        private void OnError(Control control) => control.BackColor = Color.Red;
        private void OnEdit(object sender, EventArgs args) => ((Control)sender).BackColor = Color.White;

        private bool ComboBoxHandler(ComboBox cboItem)
        {
            bool text = false;

            foreach (string item in cboItem.Items)
                if( text = item.Equals(cboItem.Text))
                {
                    Utils.log(cboItem.Name + ": " +cboItem.Text);
                    registryData.Add(cboItem.Name + ": " +cboItem.Text);
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
            registryData.Add($"{dateTimePicker.Name}: Value = {timeSpan.ToString()} ");
            Utils.log(timeSpan.ToString());
            return true;
        }

        public bool TextBoxHandler(TextBox textBox)
        {
            string text = textBox.Text;

            if (string.IsNullOrEmpty(text)) return false;

            registryData.Add($"{textBox.Name}: {text}");
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
            ))
                Utils.error(text + " is invalid date");
            
            registryData.Add($"{maskedTextBox.Name}: {text}");
            return true;
        }
    }

    public class ControlHandler
    {
        private Dictionary<string , string> textList;
        public ControlHandler(){ textList = new Dictionary<string, string>();}

        public void Add(Control ctrl) => textList.Add(ctrl.Name, ctrl.Text);

        public string this[string index]
        {
            get { return textList[index]; }
        }
    }

    public class FileHandler
    {
        private StreamReader loader;
        private StreamWriter saver;
        public FileHandler()
        {
            // loader = File.OpenRead("");
        }

        
    }

    public class DataHandler
    {
        private Dictionary<string, string> items;
        private List<string> xx;

        public DataHandler()
        {
            items = new Dictionary<string, string>();
        }

        public void Add(string data)
        {

        }

        public void Eval(string expression)
        {

        }
    }

    public class Utils
    {
        public static Backend log = data  => Console.WriteLine(data);
        public static Backend error = data => Console.WriteLine("Error: {0}", data);

    }
}
