using System;
using System.Windows;
using System.Windows.Input;

namespace Virtion.IM.View.Windows
{
    public partial class LoginWindow : Window
    {
        //private ImManager imMagr;
        public LoginWindow()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            //this.imMagr = MainWindow.imMagr;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        public static void Test()
        {
            //MainWindow.imMagr.Login("user1", "111111");
        }

        private void B_LogIn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.E_MainExpander.IsExpanded = false;
            this.PB_Progress.Visibility = Visibility.Visible;
            String userName = this.TB_UserName.Text;
            String password = this.TB_Password.Password;
            if (userName == "")
            {
                //imMagr.Login("user1", "111");
            }
            else
            {
                //imMagr.Login(userName, password);
            }
        }

    }
}
