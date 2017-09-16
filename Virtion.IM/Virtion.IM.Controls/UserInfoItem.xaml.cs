using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Virtion.IM.View.Control
{
    public delegate void UserInfoItemDoubleClickCallBack(User user);

    public partial class UserInfoItem : UserControl
    {
        public String NickName
        {
            get
            {
                return this.TB_NickName.Text;
            }
            set
            {
                this.TB_NickName.Text = value;
            }
        }
        public static UserInfoItemDoubleClickCallBack OnDoubleClickCallBack;
        public User User
        {
            get;
            set;
        }

        Storyboard myStoryboard;

        public UserInfoItem()
        {
            InitializeComponent();
            this.MouseDown += this.OnDoubleClick;

            ThicknessAnimation ta = new ThicknessAnimation();
            ta.From = new Thickness(-300, 0, 0, 0);
            ta.To = new Thickness(0, 0, 0, 0);
            ta.Duration = TimeSpan.FromSeconds(0.4);
            Storyboard.SetTarget(ta, this.context);
            Storyboard.SetTargetProperty(ta, new PropertyPath(Window.MarginProperty));

            myStoryboard = new Storyboard();
            myStoryboard.Children.Add(ta);
        }

        public void SetNickName(String nickName)
        {
            this.NickName = nickName;
        }

        public static UserInfoItem GotyeUserToUserInfoItem(User user)
        {
            UserInfoItem item = new UserInfoItem();
            item.NickName = user.NickName;
            item.User = user;
            return item;
        }

        public static void SetDoubleClickCallBack(UserInfoItemDoubleClickCallBack doubleClickCallBack)
        {
            UserInfoItem.OnDoubleClickCallBack += doubleClickCallBack;
        }

        private void OnDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (UserInfoItem.OnDoubleClickCallBack != null)
                {
                    UserInfoItem.OnDoubleClickCallBack(this.User);
                }
            }
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            this.context.Background = new SolidColorBrush(Colors.Aqua);
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            this.context.Background = new SolidColorBrush(Color.FromArgb(180, 0, 0, 0));
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            myStoryboard.Begin();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.imMagr.DeleteFriend(this.User);
        }

    }
}
