using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Popflash_Admin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void InputTextBox_TextChanged(object sender, EventArgs e)
        {
            string input = inputTextBox.Text;
            string pattern = "(setpos( -?\\d\\.?\\d){3}; setang( -?\\d\\.?\\d){3})";

            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                string[] setpos = inputTextBox.Text.Split(';');
                string[] elements = setpos[0].Split(' ');

                string zString;
                double zValue;

                if (elements.Length >= 3)
                {
                    zString = elements[3];

                    if (double.TryParse(zString, out zValue))
                    {
                        zValue -= 64.0;

                        string outputText = "alias \"specsmoke\" \"" + inputTextBox.Text.Replace(" " + zString + "; setang",
                                                                                                  " " + zValue.ToString() + "; setang") + "\"";

                        outputTextBox.Text = outputText;
                        historyTextBox.AppendText(DateTime.Now.ToString("[HH:mm:ss] ") + outputText + Environment.NewLine);

                        outputTextBox.Focus();

                        if (autoCopyCheckBox.Checked)
                        {
                            Clipboard.SetText(outputText);
                        };
                    };
                };
            };
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            inputTextBox.Text = null;
            outputTextBox.Text = null;
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(outputTextBox.Text);
        }

        private void AlwaysOnTopCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (alwaysOnTopCheckBox.Checked)
            {
                Form1.ActiveForm.TopMost = true;
            }
            else
            {
                Form1.ActiveForm.TopMost = false;
            }
        }

        private void InputOutputTextBox_Enter(object sender, EventArgs e)
        {
            var textBox = (TextBox)sender;

            BeginInvoke((Action)textBox.SelectAll);
        }
    }
}
