using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoneyTextBox
{
    public partial class CurrencyTextBox : TextBox
    {
        private decimal _textNumber = 0;
        private string _currencySymbol = "";
        private int cursorPosition = 0;
        private bool AllowEvent = true;
        /// <summary>
        /// Entered Number 
        /// </summary>
        public decimal TextasNumber
        {
            get { return _textNumber; }
            private set { _textNumber = value; }
        }

        public string CurrencySymbol
        {
            get { return _currencySymbol; }
            private set { _currencySymbol = value; }
        }

        public CurrencyTextBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Formats the entered text.
        /// </summary>
        /// <returns></returns>
        protected override void OnTextChanged(EventArgs e)
        {
            try
            {
                this.TextasNumber = this.Text.Any(Char.IsDigit) ? Convert.ToDecimal(new string(this.Text.Where(Char.IsDigit).ToArray())) : 0;

                if (!String.IsNullOrEmpty(this.Text) && AllowEvent)
                {
                    cursorPosition = this.SelectionStart;
                    int countCommas = this.Text.LastIndexOf(',');

                    AllowEvent = false;
                    this.Text = string.Format("{0:n0}", Convert.ToDecimal(this.TextasNumber)) + CurrencySymbol;
                    AllowEvent = true;

                    this.SelectionStart = countCommas >= this.Text.LastIndexOf(',') ? cursorPosition : cursorPosition + 1;
                }

                base.OnTextChanged(e);
            }
            catch (Exception ll)
            {
                MessageBox.Show(ll.Message);
            }
        }

        /// <summary>
        /// Formats the entered text.
        /// </summary>
        /// <returns></returns>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            try
            {
                if (!Char.IsDigit(e.KeyChar))
                {
                    if (!(e.KeyChar == Convert.ToChar(Keys.Back) || e.KeyChar == '.'))
                        e.Handled = true;
                }

                base.OnKeyPress(e);
            }
            catch (Exception ll)
            {
                MessageBox.Show(ll.Message);
            }
        }
    }
}
