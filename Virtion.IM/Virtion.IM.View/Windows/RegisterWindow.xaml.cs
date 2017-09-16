using System.Windows;

namespace Virtion.IM.View.Windows
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void CancelButtton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            //User user = new User()
            //{
            //    Username = this.NameText.Text,
            //    NickName = this.NickNameText.Text,
            //    Telphone = this.TelphoneText.Text,
            //};
            //if (this.ManRadio.IsChecked == true)
            //{
            //    user.Sex = "man";
            //}
            //else
            //{
            //    user.Sex = "feman";
            //}
            //MainWindow.imMagr.RegisterUser(user,this.PasswordText.Text);       
            //this.MessageText.Text = "注册成功";
        }
    }
}
