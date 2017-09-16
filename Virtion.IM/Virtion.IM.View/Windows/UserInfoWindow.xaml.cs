using System.Windows;

namespace Virtion.IM.View.Windows
{
    /// <summary>
    /// AlterInfoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UserInfoWindow : Window
    {
        public UserInfoWindow()
        {
            InitializeComponent();
        }

        public void ResetPostion()
        {
            POINT p = new POINT();
            if (ApiInvoke.GetCursorPos(out p))//API方法
            {
                //this.Left = MainWindow.friendListWindow.Left - this.Width;
                var top= p.Y - this.Height / 2;
                if( top <0)
                {
                    top=0;
                }
                this.Top = top;
            }
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ResetPostion();
        }

    }
}
