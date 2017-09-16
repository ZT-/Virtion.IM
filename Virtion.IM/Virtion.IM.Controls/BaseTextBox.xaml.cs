using System;
using System.Windows;
using System.Windows.Controls;

namespace Virtion.IM.Controls
{
    public partial class BaseTextBox : UserControl
    {
        public String Text
        {
            set
            {
                this.TB_Text.Text = value;
            }
            get
            {
                return this.TB_Text.Text;
            }

        }
        public String TipText
        {
            set
            {
                this.TB_HideText.Text = value;
            }
        }

        public BaseTextBox()
        {
            InitializeComponent();
        }

        private void TB_Text_GotFocus(object sender, RoutedEventArgs e)
        {
            this.TB_HideText.Visibility = Visibility.Hidden;
        }

        private void TB_Text_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.TB_Text.Text == String.Empty)
            {
                this.TB_HideText.Visibility = Visibility.Visible;
            }
        }
    }
}
