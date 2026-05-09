using System;
using System.Windows.Forms;

namespace Gym_Management_System.Forms
{
    public class InputDialog : Form
    {
        private TextBox txt;
        private Button btnOk;
        private Button btnCancel;
        public string Value => txt.Text;

        public InputDialog(string title, string prompt)
        {
            this.Text = title;
            this.Width = 400;
            this.Height = 150;
            Label lbl = new Label() { Left = 10, Top = 10, Text = prompt, AutoSize = true };
            txt = new TextBox() { Left = 10, Top = 35, Width = 360 };
            btnOk = new Button() { Text = "OK", Left = 210, Width = 75, Top = 70, DialogResult = DialogResult.OK };
            btnCancel = new Button() { Text = "Cancel", Left = 295, Width = 75, Top = 70, DialogResult = DialogResult.Cancel };
            this.Controls.Add(lbl);
            this.Controls.Add(txt);
            this.Controls.Add(btnOk);
            this.Controls.Add(btnCancel);
            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;
        }
    }
}
