using System;
using System.Windows;
using System.Windows.Controls;

namespace Virtion.IM.Controls
{
    public partial class PasswordBox : UserControl
    {
        public String Password
        {
            set
            {
                this.PB_Text.Password = value;
            }
            get
            {
                return this.PB_Text.Password;
            }

        }
        public String TipText
        {
            set
            {
                this.TB_HideText.Text = value;
            }
        }
        public PasswordBox()
        {
            InitializeComponent();
        }

        private void PB_Text_GotFocus(object sender, RoutedEventArgs e)
        {
            this.TB_HideText.Visibility = Visibility.Hidden;
        }

        private void PB_Text_LostFocus(object sender, RoutedEventArgs e)
        {
            if (this.PB_Text.Password == String.Empty)
            {
                this.TB_HideText.Visibility = Visibility.Visible;
            }
        }
    }
}
