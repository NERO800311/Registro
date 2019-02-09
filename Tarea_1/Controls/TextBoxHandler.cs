using System.Windows.Forms;

namespace Tarea_1.Controls
{
    public class TextBoxHandler : AbstractTextBox
    {
        private TextBox textBox;
        public TextBoxHandler(TextBox textBox) : base(textBox)
        {
            this.textBox = textBox;
        }
    }
}