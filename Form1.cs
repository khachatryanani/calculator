using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace MyCalculator
{
    public partial class Calculator : Form
    {
        public Calculator()
        {
            InitializeComponent();
            this.ActiveControl = label1;
            label1.Focus();
        }
        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        private void Output_TextChanged(object sender, EventArgs e)
        {
            HideCaret(Output.Handle);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (cleared || Output.Text == "0")
            {
                Output.Clear();
                if ((sender as Button) == dot)
                {
                    Output.Text = "0.";
                    cleared = false;
                    return;
                }
            }

            if (Output.Text.Contains('.') && (sender as Button) == dot)
            {
                return;
            }
            Output.Text = Output.Text + (sender as Button).Text;
            cleared = false;
            this.ActiveControl = label1;
            label1.Focus();
        }

        private void Calculator_KeyDown(object sender, KeyEventArgs e)
        {
            char ch = Convert.ToChar(e.KeyCode);
            if (!(char.IsDigit(ch)) && !(char.IsControl(ch)))
            {
                e.Handled = true;
            }
            switch (e.KeyCode)
            {
                case Keys.D1:
                case Keys.NumPad1:
                    button1.PerformClick();
                    break;
                case Keys.D2:
                case Keys.NumPad2:
                    button2.PerformClick();
                    break;
                case Keys.D3:
                case Keys.NumPad3:
                    button3.PerformClick();
                    break;
                case Keys.D4:
                case Keys.NumPad4:
                    button4.PerformClick();
                    break;
                case Keys.D5:
                case Keys.NumPad5:
                    button5.PerformClick();
                    break;
                case Keys.D6:
                case Keys.NumPad6:
                    button6.PerformClick();
                    break;
                case Keys.D7:
                case Keys.NumPad7:
                    button7.PerformClick();
                    break;
                case Keys.D8:
                case Keys.NumPad8:
                    button8.PerformClick();
                    break;
                case Keys.D9:
                case Keys.NumPad9:
                    button9.PerformClick();
                    break;
                case Keys.D0:
                case Keys.NumPad0:
                    button0.PerformClick();
                    break;
                case Keys.Decimal:
                case Keys.OemPeriod:
                    dot.PerformClick();
                    break;
                case Keys.Add:
                case Keys.Oemplus:
                    add.PerformClick();
                    break;
                case Keys.Subtract:
                case Keys.OemMinus:
                    sub.PerformClick();
                    break;
                case Keys.Multiply:
                    mul.PerformClick();
                    break;
                case Keys.Divide:
                    div.PerformClick();
                    break;
                case Keys.Enter:
                    eq.PerformClick();
                    break;
                case Keys.Back:
                    if (Output.Text.Length == 1 && !cleared)
                    {
                        Output.Text = "0";
                    }
                    else if (Output.Text.Length > 1 && !cleared)
                    {
                        Output.Text = Output.Text.Remove(Output.Text.Length - 1, 1);
                    }
                    break;
            }
        }

        private void DoMath(char mathOperator)
        {
            switch (mathOperator)
            {
                case '+':
                    Output.Text = obj.Add(numberMemory, Convert.ToDouble(Output.Text)).ToString();
                    break;
                case '-':
                    Output.Text = obj.Min(numberMemory, Convert.ToDouble(Output.Text)).ToString();
                    break;
                case '*':
                    Output.Text = obj.Mul(numberMemory, Convert.ToDouble(Output.Text)).ToString();
                    break;
                case '/':
                    Output.Text = obj.Div(numberMemory, Convert.ToDouble(Output.Text)).ToString();
                    break;
            }
            try
            {
                numberMemory = Convert.ToDouble(Output.Text);
                cleared = false;
            }
            catch (FormatException)
            {
                numberMemory = 0;
            }
        }

        private void Operator_Click(object sender, EventArgs e)
        {
            DoMath(charMemory);
            charMemory = Convert.ToChar((sender as Button).Text);
            cleared = true;
            this.ActiveControl = label1;
            label1.Focus();
        }

        private void clear_Click(object sender, EventArgs e)
        {
            numberMemory = 0;
            charMemory = '=';
            cleared = true;
            Output.Text = "0";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}

